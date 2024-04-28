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
                throw new HandledExeption("Промокод не найден");

            var position = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.PositionId) ??
                throw new HandledExeption("Позиция не найдена");

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
                throw new HandledExeption("Пользователь не найден");

            if (user.Payments.Any(x => x.PromoCodeId == promoCode.Id))
                throw new HandledExeption("Промокод уже использовался");

            var discount = position.Price * ((decimal)promoCode.Discount / 100);
            var newPrice = (int)(position.Price - discount);

            return newPrice;
        }
    }
}
