using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Service.ControllerService.Common;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.ControllerService.Service.Payment.GooglePlay.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public record SubscriptionNotification(string version, int notificationType, string purchaseToken, string subscriptionId);
        public class VoidedPurchaseNotification(string purchaseToken, string orderId, int productType, int refundType);
        public record CallbackData(string version, string packageName, string eventTimeMillis, SubscriptionNotification subscriptionNotification, VoidedPurchaseNotification voidedPurchaseNotification);

        public record AutoRenewingPlan(bool autoRenewEnabled);
        public record LineItem(string productId, DateTime expiryTime, AutoRenewingPlan autoRenewingPlan, OfferDetails offerDetails);
        public record OfferDetails(string basePlanId);
        public record PurchaseData(string kind, DateTime startTime, string regionCode, string subscriptionState, string latestOrderId, string acknowledgementState, List<LineItem> lineItems);

        public async Task<bool> HandlingAsync(Request request)
        {
            var base64EncodedBytes = Convert.FromBase64String(request.message.data);
            var dataJsonString = Encoding.UTF8.GetString(base64EncodedBytes);
            var callbackData = JsonConvert.DeserializeObject<CallbackData>(dataJsonString);
            logger.Information($"Получен callback от Google Play: {dataJsonString}");


            var user = await repositoryProvider.UserRepository.GetBySubscribeTokenAsync(callbackData.subscriptionNotification.purchaseToken) 
                ?? throw new Exception("Пользователь не найден");

            if(callbackData.subscriptionNotification != null)
            {
                var purchaseData = await GooglePlayHelper.GetPurchaseDataAsync(callbackData.subscriptionNotification.purchaseToken);

                logger.Information($"Получены данные о покупке: {JsonConvert.SerializeObject(purchaseData)}");
            }
            else if(callbackData.voidedPurchaseNotification != null)
            {
                //user.SubscribeToken = string.Empty;
                //user.SubscribeType = Core.Model.User.SubscribeType.None;
                await repositoryProvider.UserRepository.UpdateAsync(user);

                logger.Information($"Пользователь {user.Login} отменил подписку");
            }
            else
            {
                logger.Error($"Необработанное уведомление {dataJsonString}");
            }

            return true;
        }
    }
}
