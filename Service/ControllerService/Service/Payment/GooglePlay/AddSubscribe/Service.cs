using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Service.ControllerService.Common;
using System.Net.Http.Headers;

namespace Service.ControllerService.Service.Payment.GooglePlay.AddSubscribe
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден", true);

            // Путь к файлу ключа JSON сервисного аккаунта
            string jsonKeyFilePath = "wwwroot/keyfile.json";
            // Область доступа, необходимая для вашего запроса
            string[] scopes = new string[] { "https://www.googleapis.com/auth/androidpublisher" };

            GoogleCredential credential = GoogleCredential.FromFile(jsonKeyFilePath)
            .CreateScoped(scopes);

            var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            var httpClient = new HttpClient();

            //var response = await httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", content);
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorResponse = await response.Content.ReadAsStringAsync();
            //    logger.Information(errorResponse);
            //    throw new ApplicationException("Не удалось получить токен.");
            //}

            //var responseString = await response.Content.ReadAsStringAsync();
            //var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync($"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/com.lockvpnandroidapp/purchases/subscriptionsv2/tokens/{request.SubscribeToken}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                logger.Information(errorResponse);
                throw new ApplicationException("Не удалось получить токен.");
            }
            var data = await response.Content.ReadAsStringAsync();

            logger.Information(data);

            return true;
        }

        public class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }
    }
}
