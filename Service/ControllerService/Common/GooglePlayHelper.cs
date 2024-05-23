using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Service.ControllerService.Common
{
    public static class GooglePlayHelper
    {
        public record AutoRenewingPlan(bool autoRenewEnabled);
        public record LineItem(string productId, DateTime expiryTime, AutoRenewingPlan autoRenewingPlan, OfferDetails offerDetails);
        public record OfferDetails(string basePlanId);
        public record PurchaseData(string kind, DateTime startTime, string regionCode, string subscriptionState, string latestOrderId, string acknowledgementState, List<LineItem> lineItems);

        public static async Task<PurchaseData> GetPurchaseDataAsync(string token)
        {
            string jsonKeyFilePath = "wwwroot/keyfile.json";
            string[] scopes = ["https://www.googleapis.com/auth/androidpublisher"];
            GoogleCredential credential = GoogleCredential
                .FromFile(jsonKeyFilePath)
                .CreateScoped(scopes);
            var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync($"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/com.lockvpnandroidapp/purchases/subscriptionsv2/tokens/{token}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(errorResponse);
            }
            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PurchaseData>(data);
        }
    }
}
