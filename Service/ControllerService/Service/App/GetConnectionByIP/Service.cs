﻿using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.HttpClientService.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Serilog;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetConnectionByIP
{
    internal class Service(IRepositoryProvider repositoryProvider, ILogger logger) : IControllerService<Request, Result>
    {

        public record StreamSettings(string Network, string Security, RealitySettings RealitySettings);
        public record RealitySettings(string Dest, List<string> ServerNames, string PrivateKey, List<string> ShortIds, Settings Settings);
        public record Settings(string PublicKey, string Fingerprint);

        public async Task<Result> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);

            if ((user.AccessEndDate == null || user.AccessEndDate?.Date <= DateTime.Now.Date) && request.OS == "android")
            {
                var location = await repositoryProvider.LocationRepository.GetByServerIpAsync(request.Ip) ??
                    throw new HandledException("Локация не найдена не найден");
                var server = location.VpnServers.FirstOrDefault(x => x.Ip == request.Ip) ??
                    throw new HandledException("Сервер не найден");

                var userConnection = user.UserConnections.FirstOrDefault(x => x.VpnServerId == server.Id && x.ConnectionType == ConnectionType.Free);

                if (userConnection == null)
                {
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    var httpHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpHandler);
                    httpClient.BaseAddress = new Uri($"http://{server.Ip}:{server.Port}");
                    var authData = new MultipartFormDataContent
                    {
                        { new StringContent(server.UserName), "username" },
                        { new StringContent(server.Password), "password" }
                    };

                    var authResponse = await httpClient.PostAsync("/login", authData);

                    if (!authResponse.IsSuccessStatusCode)
                        throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

                    var authContent = await authResponse.Content.ReadAsStringAsync();
                    var authResult = bool.Parse(JObject.Parse(authContent)["success"].ToString());

                    if (!authResult)
                        throw new HandledException("Ошибка авторизации на сервере", true);

                    //Перед созданием, на всякий случай, удаляем пользователя из подключения
                    await httpClient.PostAsync($"/panel/inbound/1/delClient/{user.Guid}", null);

                    var userConnectionDataString = JsonConvert.SerializeObject(new
                    {
                        Clients = new List<InboundUser>()
                        {
                            new InboundUser()
                            {
                                Id = user.Guid,
                                Flow = "",
                                Email = user.Login,
                                LimitIp = 1,
                                TotalGB = 0,
                                ExpiryTime = 0,
                                Enable = true,
                                Reset = 0,
                                TgId = "",
                                SubId = ""
                            }
                        }
                    }, serializerSettings);

                    var addData = new MultipartFormDataContent()
                    {
                        {new StringContent("1"), "id"},
                        {new StringContent(userConnectionDataString), "settings"},
                    };

                    var addUserConnectionResponse = await httpClient.PostAsync("/panel/inbound/addClient", addData);

                    if (!addUserConnectionResponse.IsSuccessStatusCode)
                        throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

                    var addUserConnectionContent = await addUserConnectionResponse.Content.ReadAsStringAsync();
                    var addUserConnectionResult = bool.Parse(JObject.Parse(addUserConnectionContent)["success"].ToString());

                    if (!addUserConnectionResult)
                    {
                        logger.Error($"Ошибка добавления пользователя {user.Login} на сервер {server.Ip}");
                        throw new HandledException("Ошибка добавления пользователя на сервер");
                    }

                    var inboundResponse = await httpClient.GetAsync("/panel/api/inbounds/get/1");

                    if (!inboundResponse.IsSuccessStatusCode)
                        throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

                    var inboundContent = await inboundResponse.Content.ReadAsStringAsync();
                    var inboundResult = JObject.Parse(inboundContent)["obj"].ToString();

                    //Имя
                    var remark = JObject.Parse(inboundResult)["remark"].ToString();
                    var port = int.Parse(JObject.Parse(inboundResult)["port"].ToString());
                    var protocol = JObject.Parse(inboundResult)["protocol"].ToString();

                    //Остальные настройки
                    var streamSettings = JsonConvert.DeserializeObject<StreamSettings>(JObject.Parse(inboundResult)["streamSettings"].ToString());

                    userConnection = new UserConnection(server.Id, port, streamSettings.Network, protocol, streamSettings.Security, streamSettings.RealitySettings.Settings.PublicKey, 
                        streamSettings.RealitySettings.Settings.Fingerprint, streamSettings.RealitySettings.ServerNames.FirstOrDefault(), streamSettings.RealitySettings.ShortIds.FirstOrDefault(),
                        DateTime.MinValue, ConnectionType.Free);

                    user.UserConnections.Add(userConnection);
                }

                user.UpdateLastConnectionDate(DateTime.Now);
                await repositoryProvider.UserRepository.UpdateAsync(user);

                var todayStatistic = server.Statistics.FirstOrDefault(x => x.Date.Date == DateTime.Now.Date);
                if (todayStatistic != null)
                    todayStatistic.Count += 1;
                else
                    server.Statistics.Add(new Core.Model.Location.ConnectionStatistic(0, DateTime.Now.Date, 1));

                await repositoryProvider.LocationRepository.UpdateAsync(location);

                return new Result()
                {
                    Name = $"%2F#subscribe-access-{user.Login}",
                    Ip = server.Ip,
                    Port = userConnection.Port,
                    Network = userConnection.Network,
                    Protocol = userConnection.Protocol,
                    Security = userConnection.Security,
                    PublicKey = userConnection.PublicKey,
                    Fingerprint = userConnection.Fingerprint,
                    ServerName = userConnection.ServerName,
                    ShortId = userConnection.ShortId,
                    Guid = user.Guid.ToString()
                };
            }
            else
            {
                if (user.AccessEndDate == null || user.AccessEndDate?.Date < DateTime.Now.Date)
                    throw new HandledException("Ваша подписка закончилась");

                var location = await repositoryProvider.LocationRepository.GetByServerIpAsync(request.Ip) ??
                    throw new HandledException("Локация не найдена не найден");
                var server = location.VpnServers.FirstOrDefault(x => x.Ip == request.Ip) ??
                    throw new HandledException("Сервер не найден");

                var userConnection = user.UserConnections.FirstOrDefault(x => x.VpnServerId == server.Id && x.ConnectionType == ConnectionType.Paid);

                if (userConnection == null || userConnection.AccessEndDate != user.AccessEndDate)
                {
                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    var httpHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpHandler);
                    httpClient.BaseAddress = new Uri($"http://{server.Ip}:{server.Port}");
                    var authData = new MultipartFormDataContent
                    {
                        { new StringContent(server.UserName), "username" },
                        { new StringContent(server.Password), "password" }
                    };

                    var authResponse = await httpClient.PostAsync("/login", authData);

                    if (!authResponse.IsSuccessStatusCode)
                        throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

                    var authContent = await authResponse.Content.ReadAsStringAsync();
                    var authResult = bool.Parse(JObject.Parse(authContent)["success"].ToString());

                    if (!authResult)
                        throw new HandledException("Ошибка авторизации на сервере", true);

                    //Перед созданием, на всякий случай, удаляем пользователя из подключения
                    await httpClient.PostAsync($"/panel/inbound/1/delClient/{user.Guid}", null);

                    var expiryTime = (long)user.AccessEndDate?.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

                    var userConnectionDataString = JsonConvert.SerializeObject(new
                    {
                        Clients = new List<InboundUser>()
                        {
                            new InboundUser()
                            {
                                Id = user.Guid,
                                Flow = "",
                                Email = user.Login,
                                LimitIp = 3,
                                TotalGB = 0,
                                ExpiryTime = expiryTime,
                                Enable = true,
                                Reset = 0,
                                TgId = "",
                                SubId = ""
                            }
                        }
                    }, serializerSettings);

                    var addData = new MultipartFormDataContent()
                    {
                        {new StringContent("1"), "id"},
                        {new StringContent(userConnectionDataString), "settings"},
                    };

                    var addUserConnectionResponse = await httpClient.PostAsync("/panel/inbound/addClient", addData);

                    if (!addUserConnectionResponse.IsSuccessStatusCode)
                        throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

                    var addUserConnectionContent = await addUserConnectionResponse.Content.ReadAsStringAsync();
                    var addUserConnectionResult = bool.Parse(JObject.Parse(addUserConnectionContent)["success"].ToString());

                    if (!addUserConnectionResult)
                    {
                        logger.Error($"Ошибка добавления пользователя {user.Login} на сервер {server.Ip}");
                        throw new HandledException("Ошибка добавления пользователя на сервер");
                    }

                    var inboundResponse = await httpClient.GetAsync("/panel/api/inbounds/get/1");

                    if (!inboundResponse.IsSuccessStatusCode)
                        throw new HandledException("Произошла ошибка или сервер временно недоступен", true);

                    var inboundContent = await inboundResponse.Content.ReadAsStringAsync();
                    var inboundResult = JObject.Parse(inboundContent)["obj"].ToString();

                    //Имя
                    var remark = JObject.Parse(inboundResult)["remark"].ToString();
                    var port = int.Parse(JObject.Parse(inboundResult)["port"].ToString());
                    var protocol = JObject.Parse(inboundResult)["protocol"].ToString();

                    //Остальные настройки
                    var streamSettings = JsonConvert.DeserializeObject<StreamSettings>(JObject.Parse(inboundResult)["streamSettings"].ToString());

                    userConnection = new UserConnection(server.Id, port, streamSettings.Network, protocol, streamSettings.Security, streamSettings.RealitySettings.Settings.PublicKey, streamSettings.RealitySettings.Settings.Fingerprint,
                        streamSettings.RealitySettings.ServerNames.FirstOrDefault(), streamSettings.RealitySettings.ShortIds.FirstOrDefault(), user.AccessEndDate ?? throw new Exception("Доступ не может быть пустым"), ConnectionType.Paid);

                    user.UserConnections.Add(userConnection);
                }

                user.UpdateLastConnectionDate(DateTime.Now);
                await repositoryProvider.UserRepository.UpdateAsync(user);

                var todayStatistic = server.Statistics.FirstOrDefault(x => x.Date.Date == DateTime.Now.Date);
                if (todayStatistic != null)
                    todayStatistic.Count += 1;
                else
                    server.Statistics.Add(new Core.Model.Location.ConnectionStatistic(0, DateTime.Now.Date, 1));

                await repositoryProvider.LocationRepository.UpdateAsync(location);

                return new Result()
                {
                    Name = $"%2F#subscribe-access-{user.Login}",
                    Ip = server.Ip,
                    Port = userConnection.Port,
                    Network = userConnection.Network,
                    Protocol = userConnection.Protocol,
                    Security = userConnection.Security,
                    PublicKey = userConnection.PublicKey,
                    Fingerprint = userConnection.Fingerprint,
                    ServerName = userConnection.ServerName,
                    ShortId = userConnection.ShortId,
                    Guid = user.Guid.ToString()
                };
            }
        }
    }
}
