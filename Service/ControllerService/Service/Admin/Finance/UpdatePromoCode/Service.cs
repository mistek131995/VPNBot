using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.UpdatePromoCode
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var promoCode = await repositoryProvider.PromoCodeRepository.GetByIdAsync(request.Id)
                ?? throw new HandledExeption("Промокод не найден");

            promoCode.Code = request.Code;
            promoCode.Discount = request.Discount;
            promoCode.UsageCount = request.UsageCount;
            promoCode.StartDate = request.StartDate;
            promoCode.EndDate = request.EndDate;

            await repositoryProvider.PromoCodeRepository.UpdateAsync(promoCode);

            return true;
        }
    }
}
