using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.GetAccessPosition
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var position = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id)
                ?? throw new HandledException("Позиция не найдена");

            return new Result()
            {
                Id = position.Id,
                Name = position.Name,
                Description = position.Description,
                GooglePlayIdentifier = position.GooglePlayIdentifier,
                MonthCount = position.MonthCount,
                Price = position.Price,
            };
        }
    }
}
