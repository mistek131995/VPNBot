using Database.Common;
using Database.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;
using VpnBotApi.Worker.TelegramBot.WebClientRepository.Model;

namespace VpnBotApi.Worker.TelegramBot.WebClientRepository
{
    public class TelegramBotWebClient(IRepositoryProvider repositoryProvider)
    {
        private readonly IRepositoryProvider repositoryProvider = repositoryProvider;

        private VpnServer vpnServer;
        private Cookie authCookie;

        /// <summary>
        /// Получаем куку аутентификации
        /// </summary>
        /// <returns></returns>
        private async Task GetAuthCookie(string ip, int port)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", "mistek"),
                        new KeyValuePair<string, string>("password", "Omega131995@")

                    });

                    var result = await client.PostAsync($"http://{ip}:{port}/login", content);

                    if (result.IsSuccessStatusCode)
                    {
                        authCookie = handler.CookieContainer.GetCookies(new Uri($"http://{ip}:2001")).FirstOrDefault();
                    }
                }
            }
        }

        #region Создание доступа в VPN

        public async Task<Inbound> CreateNewAccess(long telegramUserId, DateTime endDate)
        {
            //Получаем сервер с наименьшим кол-вом пользователей
            if(vpnServer == null)
            {
                vpnServer = await repositoryProvider.VpnServerRepository.GetWithMinUserCountAsync()
                ?? throw new Exception("Произошла ошибка, не удалось получить VPN сервер.");
            }

            //Если кука не была получена, получаем ее
            if (authCookie == null)
            {
                await GetAuthCookie(vpnServer.Ip, vpnServer.Port);
            }

            //Тут удаляем старое подключение если оно есть
            var oldAccess = await repositoryProvider.AccessRepository.GetByTelegramUserIdAsync(telegramUserId);
            if (oldAccess != null)
                await DeleteInbound(oldAccess.Guid);

            //Тут добавляем новое подключение
            var guid = await AddUserToInbound(telegramUserId, endDate);

            var inbound = await GetInbound();
            inbound.Guid = guid;
            inbound.EndDate = endDate;
            inbound.AccessName += $"-{telegramUserId}";

            return inbound;
        }

        /// <summary>
        /// Добавляет пользователя в подключение
        /// </summary>
        /// <param name="telegramUserId"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private async Task<Guid> AddUserToInbound(long telegramUserId, DateTime endDate)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(authCookie);

            var expiryTime = endDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            var newVpnUser = new InboundUser
            {
                Id = Guid.NewGuid(),
                Flow = "",
                Email = telegramUserId.ToString(),
                LimitIp = 1,
                TotalGB = 0,
                ExpiryTime = (long)expiryTime,
                Enable = true,
            };

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                    var settings = new
                    {
                        clients = new InboundUser[]
                        {
                            newVpnUser
                        }
                    };

                    var serializerSettings = new JsonSerializerSettings();
                    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("id", "1"),
                        new KeyValuePair<string, string>("settings", JsonConvert.SerializeObject(settings, serializerSettings))
                    });

                    var response = await client.PostAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/addClient", content);

                    //Если мы успешно добавили пользователя, нужно увеличить кол-во в БД
                    if (response.IsSuccessStatusCode)
                    {
                        vpnServer.UserCount += 1;
                        await repositoryProvider.VpnServerRepository.UpdateVpnServerAsync(vpnServer);
                    }
                }
            }

            return newVpnUser.Id;
        }

        private async Task<Inbound> GetInbound()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(authCookie);

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/get/1");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        var deserializeObject = JObject.Parse(responseString);
                        var obj = deserializeObject["obj"];

                        var inbound = JsonConvert.DeserializeObject<Inbound>(obj["streamSettings"].ToString());
                        inbound.Ip = vpnServer.Ip;
                        inbound.Port = int.Parse(obj["port"].ToString());
                        inbound.AccessName = obj["remark"].ToString();

                        return inbound;
                    }
                }
            }

            throw new Exception("Неудалось создать подключение.");
        }

        private async Task DeleteInbound(Guid guid)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(authCookie);

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using (var client = new HttpClient(handler))
                {
                    var response = await client.PostAsync($"http://{vpnServer.Ip}:{vpnServer.Port}/panel/api/inbounds/1/delClient/{guid}", null);
                }
            }
        }

        #endregion


        #region Обновление доступа

        public async Task UpdateAccessDateAsync(long telegramUserId, DateTime endDate)
        {
            var access = await repositoryProvider.AccessRepository.GetByTelegramUserIdAsync(telegramUserId);

            if (authCookie == null)
            {
                await GetAuthCookie(access.VpnServer.Ip, access.VpnServer.Port);
            }

            var expiryTime = endDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            var vpnUser = new InboundUser
            {
                Id = access.Guid,
                Flow = "",
                Email = telegramUserId.ToString(),
                LimitIp = 1,
                TotalGB = 0,
                ExpiryTime = (long)expiryTime,
                Enable = true,
            };

            var cookieContainer = new CookieContainer();
            cookieContainer.Add(authCookie);

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            {
                using(var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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

                    var response = await client.PostAsync($"http://{access.VpnServer.Ip}:{access.VpnServer.Port}/panel/api/inbounds/updateClient/{access.Guid}", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Неудалось обновить подключение на VPN сервере.");
                    }
                }
            }
        }

        #endregion
    }
}
