using Google.Apis.Auth;
using Newtonsoft.Json;

namespace Service.ControllerService.Common.Extensions
{
    public static class GoogleAuth
    {
        public async static Task<GoogleJsonWebSignature.Payload> ValidateAccessToken(string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={token}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GoogleJsonWebSignature.Payload>(json);
            }

            throw new InvalidJwtException("Invalid google token");
        }
    }
}
