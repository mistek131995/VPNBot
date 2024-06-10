using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.HttpClientService.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetConnectionByIP
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
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
                        throw new HandledException("Ошибка добавления пользователя на сервер", true);

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

                    userConnection = new UserConnection()
                    {
                        VpnServerId = server.Id,
                        Port = port,
                        Network = streamSettings.Network,
                        Protocol = protocol,
                        Security = streamSettings.Security,
                        PublicKey = streamSettings.RealitySettings.Settings.PublicKey,
                        Fingerprint = streamSettings.RealitySettings.Settings.Fingerprint,
                        ServerName = streamSettings.RealitySettings.ServerNames.FirstOrDefault(),
                        ShortId = streamSettings.RealitySettings.ShortIds.FirstOrDefault(),
                        ConnectionType = ConnectionType.Free,

                        AccessEndDate = DateTime.MinValue
                    };

                    user.UserConnections.Add(userConnection);
                }

                user.LastConnection = DateTime.Now;
                await repositoryProvider.UserRepository.UpdateAsync(user);

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
                        throw new HandledException("Ошибка добавления пользователя на сервер", true);

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

                    userConnection = new UserConnection()
                    {
                        VpnServerId = server.Id,
                        Port = port,
                        Network = streamSettings.Network,
                        Protocol = protocol,
                        Security = streamSettings.Security,
                        PublicKey = streamSettings.RealitySettings.Settings.PublicKey,
                        Fingerprint = streamSettings.RealitySettings.Settings.Fingerprint,
                        ServerName = streamSettings.RealitySettings.ServerNames.FirstOrDefault(),
                        ShortId = streamSettings.RealitySettings.ShortIds.FirstOrDefault(),
                        ConnectionType = ConnectionType.Paid,

                        AccessEndDate = user.AccessEndDate ?? throw new Exception("Доступ не может быть пустым")
                    };

                    user.UserConnections.Add(userConnection);
                }

                user.LastConnection = DateTime.Now;
                await repositoryProvider.UserRepository.UpdateAsync(user);

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
