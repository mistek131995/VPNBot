using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.Finance.GetAccessPositions
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, List<Result>>
    {
        public async Task<List<Result>> HandlingAsync(Request request)
        {
            var accessPosition = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            return accessPosition.Select(x => new Result()
            {
                Id = x.Id,
                Description = x.Description,
                GooglePlayIdentifier = x.GooglePlayIdentifier,
                MonthCount = x.MonthCount,
                Name = x.Name,
                Price = x.Price,
            }).ToList();
        }
    }
}
