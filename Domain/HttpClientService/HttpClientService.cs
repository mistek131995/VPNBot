using System.Net;
using System.Net.Http.Headers;

namespace Domain.HttpClientService
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

        public async Task<List<Guid>> DeleteInboundUserAsync(List<Guid> guids, string ip, int port, string userName, string password)
        {
            var httpClient = await GetAuthCookie(ip, port, userName, password);

            var successDelete = new List<Guid>();

            foreach (var guid in guids)
            {
                var response = await httpClient.PostAsync($"http://{ip}:{port}/panel/api/inbounds/1/delClient/{guid}", null);

                if (response.IsSuccessStatusCode)
                {
                    successDelete.Add(guid);
                }
            }

            return successDelete;
        }
    }
}
