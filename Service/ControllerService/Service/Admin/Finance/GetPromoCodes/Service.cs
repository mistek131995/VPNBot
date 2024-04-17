using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.Finance.GetPromoCodes
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var promoCodes = await repositoryProvider.PromoCodeRepository.GetAllAsync();

            return new Result
            {
                PromoCodes = promoCodes.Select(x => new Result.PromoCode
                {
                    Id = x.Id,
                    Code = x.Code,
                    Discount = x.Discount,
                    UsageCount = x.UsageCount,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList()
            };
        }
    }
}
