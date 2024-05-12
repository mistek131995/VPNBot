using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.DeletePromoCode
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var promoCode = await repositoryProvider.PromoCodeRepository.GetByIdAsync(request.Id)
                ?? throw new HandledException("Промокод не найден");

            await repositoryProvider.PromoCodeRepository.DeleteAsync(promoCode);

            return true;
        }
    }
}
