using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using Core.Model.User;
using Newtonsoft.Json;
using Infrastructure.HttpClientService.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

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

            if (user.AccessEndDate == null || user.AccessEndDate?.Date < DateTime.Now.Date)
                throw new HandledExeption("Ваша подписка закончилась");


            var location = await repositoryProvider.LocationRepository.GetByServerIpAsync(request.Ip) ??
                throw new HandledExeption("Локация не найдена не найден");
            var server = location.VpnServers.FirstOrDefault(x => x.Ip == request.Ip) ??
                throw new HandledExeption("Сервер не найден");

            var userConnection = user.UserConnections.FirstOrDefault(x => x.VpnServerId == server.Id);

            if(userConnection == null || userConnection.AccessEndDate != user.AccessEndDate)
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
                    throw new HandledExeption("Произошла ошибка или сервер временно недоступен", true);

                var authContent = await authResponse.Content.ReadAsStringAsync();
                var authResult = bool.Parse(JObject.Parse(authContent)["success"].ToString());

                if (!authResult)
                    throw new HandledExeption("Ошибка авторизации на сервере", true);

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
                    
                if(!addUserConnectionResponse.IsSuccessStatusCode)
                    throw new HandledExeption("Произошла ошибка или сервер временно недоступен", true);

                var addUserConnectionContent = await addUserConnectionResponse.Content.ReadAsStringAsync();
                var addUserConnectionResult = bool.Parse(JObject.Parse(addUserConnectionContent)["success"].ToString());

                if (!addUserConnectionResult)
                    throw new HandledExeption("Ошибка добавления пользователя на сервер", true);

                var inboundResponse = await httpClient.GetAsync("/panel/api/inbounds/get/1");

                if (!inboundResponse.IsSuccessStatusCode)
                    throw new HandledExeption("Произошла ошибка или сервер временно недоступен", true);

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

                    AccessEndDate = user.AccessEndDate ?? throw new Exception("Доступ не может быть пустым")
                };

                user.UserConnections.Add(userConnection);
                await repositoryProvider.UserRepository.UpdateAsync(user);
            }

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
            
            //else
            //{
            //    var testConnections = new List<Result>()
            //    {
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn2",
            //            Ip = "2.59.183.140",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc",
            //            Fingerprint = "chrome",
            //            ServerName = "yahoo.com",
            //            ShortId = "ab2cc97b",
            //            Guid = "4a2cc04a-2184-4311-be9c-15216ac09461"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn1",
            //            Ip = "163.5.207.114",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "LCuiKV8N1EctocogPKiyjO5bkgd7xg_hHa6eGXwEBjU",
            //            Fingerprint = "chrome",
            //            ServerName = "ferzu.com",
            //            ShortId = "0383b501",
            //            Guid = "96931db0-325b-45c4-b06d-4d23e4e0f54f"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn3",
            //            Ip = "185.9.55.87",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc",
            //            Fingerprint = "chrome",
            //            ServerName =  "yahoo.com",
            //            ShortId = "ab2cc97b",
            //            Guid = "4a2cc04a-2184-4311-be9c-15216ac09461"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn4",
            //            Ip = "195.211.96.156",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "DtTDO8U3IC4Adf4DENbd5HzE5xKEr7eqqEqEaFyf2Dg",
            //            Fingerprint = "chrome",
            //            ServerName =  "yahoo.com",
            //            ShortId = "2940ef21",
            //            Guid = "1728d17a-d8ce-424e-8432-adea8b6c5b50"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn5",
            //            Ip = "193.233.48.66",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "nfYEcQqO_xhCIT_-TfHAa2jH69fyEKRZdAbtZ5lfZw0",
            //            Fingerprint = "chrome",
            //            ServerName =  "yahoo.com",
            //            ShortId = "b542ecdb",
            //            Guid = "1b1299a5-ba86-4015-9f94-f82e678f16f3"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn6",
            //            Ip = "94.131.123.17",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "ZTxzbg5IzS_BFIiwWT_k3G5o_T56_Wh3S-DQ1XsQgCw",
            //            Fingerprint = "chrome",
            //            ServerName =  "yahoo.com",
            //            ShortId = "a8cafa07",
            //            Guid = "8454cdb5-eaca-4bcf-aa18-22a14ec15407"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn7",
            //            Ip = "78.153.139.17",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "3h2QHYeRQqiisb8PqlTgz2vh1L-FOp-cUPOyT4lgK1k",
            //            Fingerprint = "chrome",
            //            ServerName =  "yahoo.com",
            //            ShortId = "77eaf6e8",
            //            Guid = "28bcfea1-53d0-4f3c-9f06-2df96ceef89c"
            //        },
            //        new Result()
            //        {
            //            Name = "%2F#subscribe-access-vpn8",
            //            Ip = "95.164.6.175",
            //            Port = 443,
            //            Network = "tcp",
            //            Protocol = "vless",
            //            Security = "reality",
            //            PublicKey = "SvlSGOQONsCLv-BZJDGMaGGDSjKN5v8RbKDdt7kBNE4",
            //            Fingerprint = "chrome",
            //            ServerName =  "yahoo.com",
            //            ShortId = "3d127afb",
            //            Guid = "29d83cec-4312-44d1-9c59-bcea7530882a"
            //        },
            //    };

            //    return testConnections.FirstOrDefault(x => x.Ip == request.Ip)
            //        ?? throw new HandledExeption("Сервер не найден.");
            //}


        }
    }
}
