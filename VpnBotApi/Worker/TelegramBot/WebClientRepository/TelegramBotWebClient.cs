using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;
using VpnBotApi.Worker.TelegramBot.WebClientRepository.Model;

namespace VpnBotApi.Worker.TelegramBot.WebClientRepository
{
    public class TelegramBotWebClient(IConfiguration configuration)
    {
        private readonly string ip = configuration["IP"];
        private Cookie authCookie;

        /// <summary>
        /// Получаем куку аутентификации
        /// </summary>
        /// <returns></returns>
        private async Task GetAuthCookie()
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

                    var result = await client.PostAsync($"http://{ip}:2001/login", content);

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
            if (authCookie == null)
            {
                await GetAuthCookie();
            }

            //Тут добавляем пользователя в подключение в веб панели
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
                ExpiryTime = expiryTime,
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

                    var response = await client.PostAsync($"http://{ip}:2001/panel/api/inbounds/addClient", content);

                    if (response.IsSuccessStatusCode)
                    {
                        //Тут возвращаем модель подключения для добавления доступа в БД
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
                    var response = await client.GetAsync($"http://{ip}:2001/panel/api/inbounds/get/1");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        var deserializeObject = JObject.Parse(responseString);
                        var obj = deserializeObject["obj"];

                        var inbound = JsonConvert.DeserializeObject<Inbound>(obj["streamSettings"].ToString());
                        inbound.Ip = ip;
                        inbound.Port = int.Parse(obj["port"].ToString());
                        inbound.AccessName = obj["remark"].ToString();

                        return inbound;
                    }
                }
            }

            throw new Exception("Неудалось создать подключение.");
        }

        #endregion


        #region Обновление доступа

        public async Task UpdateAccessDateAsync(Guid guid, long telegramUserId, DateTime endDate)
        {
            if (authCookie == null)
            {
                await GetAuthCookie();
            }

            var expiryTime = endDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;

            var vpnUser = new InboundUser
            {
                Id = guid,
                Flow = "",
                Email = telegramUserId.ToString(),
                LimitIp = 1,
                TotalGB = 0,
                ExpiryTime = expiryTime,
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

                    var response = await client.PostAsync($"http://{ip}:2001/panel/api/inbounds/updateClient/{guid}", content);

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
