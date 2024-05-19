using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.GooglePlay.AddSubscribe
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден", true);

            var httpClient = new HttpClient();

            string clientId = "354549970311-bi5ordl9jsbsbl874vob4bq1ahr6r98k.apps.googleusercontent.com";
            string scope = "https://www.googleapis.com/auth/androidpublisher";
            string clientSecret = "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";

            var requestBody = new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"client_id", clientId},
                {"client_secret", clientSecret},
                {"scope", scope}
            };

            var content = new FormUrlEncodedContent(requestBody);
            var response = await httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                logger.Information(errorResponse);
                throw new ApplicationException("Не удалось получить access token.");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

            //string clientId = "354549970311-bi5ordl9jsbsbl874vob4bq1ahr6r98k.apps.googleusercontent.com";
            //string scope = "https://www.googleapis.com/auth/androidpublisher";
            //string redirectUri = "https://lockvpn.me";
            //string responseType = "code";

            //string authorizationUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&response_type={responseType}";

            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("grant_type", "authorization_code"),
            //    new KeyValuePair<string, string>("code", "4/0AdLIrYc25eVCPJDONWVxp3XkTZvOQleCec7h6sNwH2i1dHrRJFL6YwmzAINRG746AWzjjA"),
            //    new KeyValuePair<string, string>("client_id", "354549970311-bi5ordl9jsbsbl874vob4bq1ahr6r98k.apps.googleusercontent.com"),
            //    new KeyValuePair<string, string>("client_secret", "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"),
            //    new KeyValuePair<string, string>("redirect_uri", "https://lockvpn.me")
            //});

            //var response = await httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", content);
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorResponse = await response.Content.ReadAsStringAsync();
            //    logger.Information(errorResponse);
            //    throw new ApplicationException("Не удалось получить токен.");
            //}

            //var responseString = await response.Content.ReadAsStringAsync();
            //var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);

            //var response = await httpClient.GetAsync($"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/com.lockvpnandroidapp/purchases/subscriptionsv2/tokens/{request.SubscribeToken}");
            //response.EnsureSuccessStatusCode();
            //var data = await response.Content.ReadAsStringAsync();

            logger.Information(tokenResponse["access_token"]);

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
