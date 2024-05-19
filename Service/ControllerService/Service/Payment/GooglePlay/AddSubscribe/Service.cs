using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.GooglePlay.AddSubscribe
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public record AutoRenewingPlan(bool autoRenewEnabled);

        public record LineItem(string productId, DateTime expiryTime, AutoRenewingPlan autoRenewingPlan, OfferDetails offerDetails);

        public record OfferDetails(string basePlanId);

        public class Root(string kind, DateTime startTime, string regionCode, string subscriptionState, string latestOrderId, string acknowledgementState, List<LineItem> lineItems);

        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден", true);

            user.SubscribeToken = request.SubscribeToken;
            user.SubscribeType = Core.Model.User.SubscribeType.GooglePlay;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            logger.Information($"Пользователю {user.Login} успешно добавлен токен Google Play.");

            return true;
        }
    }
}
