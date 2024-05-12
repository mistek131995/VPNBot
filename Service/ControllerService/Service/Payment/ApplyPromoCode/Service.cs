using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.ApplyPromoCode
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, int>
    {
        public async Task<int> HandlingAsync(Request request)
        {
            var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(request.PromoCode) ??
                throw new HandledException("Промокод не найден");

            var position = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.PositionId) ??
                throw new HandledException("Позиция не найдена");

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
                throw new HandledException("Пользователь не найден");

            if (user.Payments.Any(x => x.PromoCodeId == promoCode.Id) && user.Role != Core.Model.User.UserRole.Admin)
                throw new HandledException("Промокод уже использовался");

            var discount = position.Price * ((decimal)promoCode.Discount / 100);
            var newPrice = (int)(position.Price - discount);

            return newPrice;
        }
    }
}
