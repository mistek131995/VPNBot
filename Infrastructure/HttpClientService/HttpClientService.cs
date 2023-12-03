﻿using Azure;
using Core.Model.User;
using Core.Model.VpnServer;
using Infrastructure.HttpClientService.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;

namespace Infrastructure.HttpClientService
{
    public class HttpClientService  : IHttpClientService
    {
        /// <summary>
        /// Получаем куку
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private async Task<HttpClient> GetAuthCookie(string ip, int port, string userName, string password)
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password)

            });

            var result = await httpClient.PostAsync($"http://{ip}:{port}/login", content);

            if (result.IsSuccessStatusCode)
            {
                var authCookie = httpClientHandler.CookieContainer.GetCookies(new Uri($"http://{ip}:2001")).FirstOrDefault();

                var cookieContainer = new CookieContainer();
                cookieContainer.Add(authCookie);

                httpClientHandler.CookieContainer.Add(authCookie);
            }

            return httpClient;
        }

        /// <summary>
        /// Удаляет список пользователей из подключения
        /// </summary>
        /// <param name="guids">Список идентификаторов пользователей</param>
        /// <param name="vpnServer">Сервер на котором удаляем</param>
        /// <returns>Возвращаем список успешно удаленных пользователей</returns>
        public async Task<List<Guid>> DeleteInboundUserAsync(List<Guid> guids, VpnServer vpnServer)
        {
            var httpClient = await GetAuthCookie(vpnServer.Ip, vpnServer.Port, vpnServer.UserName, vpnServer.Password);

            var successDelete = new List<Guid>();

            foreach (var guid in guids)
            {
                var response = await httpClient.PostAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/1/delClient/{guid}", null);

                if (response.IsSuccessStatusCode)
                {
                    successDelete.Add(guid);
                }
            }

            return successDelete;
        }

        /// <summary>
        /// Создаем пользователя в подключении
        /// </summary>
        /// <param name="user">Пользователь с частично заполненным подключением(Guid, EndDate, VpnServerId)</param>
        /// <param name="vpnServer">Сервер на котором создаем пользователя</param>
        /// <returns>Возвращает пользователя с полностью заполненным подключением</returns>
        public async Task<User> CreateInboundUserAsync(User user, VpnServer vpnServer)
        {
            var httpClient = await GetAuthCookie(vpnServer.Ip, vpnServer.Port, vpnServer.UserName, vpnServer.Password);

            //Удаляем старое подключение
            await httpClient.PostAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/1/delClient/{user.Access.Guid}", null);

            //Создаем нового пользователя для подключения
            var expiryTime = user.Access.EndDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            var newInboundUser = new InboundUser()
            {
                Id = user.Access.Guid,
                Flow = "",
                Email = user.TelegramUserId.ToString(),
                LimitIp = 1,
                TotalGB = 0,
                ExpiryTime = (long)expiryTime,
                Enable = true,
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var settings = new
            {
                clients = new InboundUser[]
                {
                    newInboundUser
                }
            };

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("id", "1"),
                new KeyValuePair<string, string>("settings", JsonConvert.SerializeObject(settings, serializerSettings))
            });

            var addClientResponse = await httpClient.PostAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/addClient", content);

            if(addClientResponse.IsSuccessStatusCode)
            {
                var inboundResponse = await httpClient.GetAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/get/1");

                if (inboundResponse.IsSuccessStatusCode)
                {
                    var responseString = await inboundResponse.Content.ReadAsStringAsync();

                    var deserializeObject = JObject.Parse(responseString);
                    var obj = deserializeObject["obj"];

                    var inbound = JsonConvert.DeserializeObject<Inbound>(obj["streamSettings"].ToString());

                    user.Access.Port = int.Parse(obj["port"].ToString());
                    user.Access.Network = inbound.Network;
                    user.Access.Security = inbound.Security;
                    user.Access.AccessName = obj["remark"].ToString();
                    user.Access.Fingerprint = inbound.RealitySettings.Settings.Fingerprint;
                    user.Access.PublicKey = inbound.RealitySettings.Settings.PublicKey;
                    user.Access.ServerName = inbound.RealitySettings.ServerNames.First();
                    user.Access.ShortId = inbound.RealitySettings.ShortIds.First();
                    user.Access.IsDeprecated = false;

                    return user;
                }
            }

            throw new Exception("Ошибка добавления пользоваеля на VPN сервер.");
        }

        /// <summary>
        /// Обновляет существующее подключение
        /// </summary>
        /// <param name="user">Пользователь с уже ОБНОВЛЕННЫМИ данными</param>
        /// <param name="vpnServer">Сервер на котором обновляем пользователя</param>
        public async Task UpdateInboundUserAsync(User user, VpnServer vpnServer)
        {
            var httpClient = await GetAuthCookie(vpnServer.Ip, vpnServer.Port, vpnServer.UserName, vpnServer.Password);

            var expiryTime = user.Access.EndDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            var vpnUser = new InboundUser
            {
                Id = user.Access.Guid,
                Flow = "",
                Email = user.TelegramUserId.ToString(),
                LimitIp = 1,
                TotalGB = 0,
                ExpiryTime = (long)expiryTime,
                Enable = true,
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var settings = new
            {
                clients = new InboundUser[]
                {
                    vpnUser
                }
            };

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("id", "1"),
                new KeyValuePair<string, string>("settings", JsonConvert.SerializeObject(settings, serializerSettings))
            });

            var response = await httpClient.PostAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/updateClient/{user.Access.Guid}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Неудалось обновить подключение на VPN сервере.");
            }
        }
    }
}
