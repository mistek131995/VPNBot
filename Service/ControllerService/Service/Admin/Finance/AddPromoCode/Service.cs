using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Finance;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.AddPromoCode
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var promoCode = await repositoryProvider.PromoCodeRepository.GetByCodeAsync(request.Code);

            if (promoCode != null)
                throw new HandledException("Промокод с таким кодом уже существует");

            await repositoryProvider.PromoCodeRepository.AddAsync(new PromoCode
            {
                Code = request.Code,
                Discount = request.Discount,
                UsageCount = request.UsageCount,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            });

            return true;
        }
    }
}
