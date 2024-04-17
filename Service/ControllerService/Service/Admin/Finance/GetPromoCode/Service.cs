using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.GetPromoCode
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var promoCode = await repositoryProvider.PromoCodeRepository.GetByIdAsync(request.Id)
                ?? throw new HandledExeption("Промокод не найден");

            return new Result
            {
                Id = promoCode.Id,
                Code = promoCode.Code,
                Discount = promoCode.Discount,
                UsageCount = promoCode.UsageCount,
                StartDate = promoCode.StartDate,
                EndDate = promoCode.EndDate
            };
        }
    }
}
