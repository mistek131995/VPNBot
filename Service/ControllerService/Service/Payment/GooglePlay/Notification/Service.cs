using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Service.ControllerService.Service.Payment.GooglePlay.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public record SubscriptionNotification(string version, int notificationType, string purchaseToken, string subscriptionId);
        public record CallbackData(string version, string packageName, string eventTimeMillis, SubscriptionNotification subscriptionNotification);

        public record AutoRenewingPlan(bool autoRenewEnabled);
        public record LineItem(string productId, DateTime expiryTime, AutoRenewingPlan autoRenewingPlan, OfferDetails offerDetails);
        public record OfferDetails(string basePlanId);
        public record PurchaseData(string kind, DateTime startTime, string regionCode, string subscriptionState, string latestOrderId, string acknowledgementState, List<LineItem> lineItems);

        public async Task<bool> HandlingAsync(Request request)
        {
            var base64EncodedBytes = Convert.FromBase64String(request.message.data);
            var dataJsonString = Encoding.UTF8.GetString(base64EncodedBytes);
            var callbackData = JsonConvert.DeserializeObject<CallbackData>(dataJsonString);
            logger.Information(Encoding.UTF8.GetString(base64EncodedBytes));


            string jsonKeyFilePath = "wwwroot/keyfile.json";
            string[] scopes = new string[] { "https://www.googleapis.com/auth/androidpublisher" };
            GoogleCredential credential = GoogleCredential
                .FromFile(jsonKeyFilePath)
                .CreateScoped(scopes);
            var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync($"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/com.lockvpnandroidapp/purchases/subscriptionsv2/tokens/{callbackData.subscriptionNotification.purchaseToken}");
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                logger.Information(errorResponse);
                throw new ApplicationException("Не удалось получить информацию о покупке.");
            }
            var data = await response.Content.ReadAsStringAsync();

            logger.Information(data);

            return true;
        }
    }
}
