using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.UpdateAccessPosition
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var position = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.Id)
                ?? throw new HandledException("Позиция не найдена");

            position.UpdateName(request.Name);
            position.UpdateDescription(request.Description);
            position.UpdatePrice(request.Price);
            position.UpdateMonthCount(request.MonthCount);
            //position.GooglePlayIdentifier = request.GooglePlayIdentifier;

            await repositoryProvider.AccessPositionRepository.UpdateAsync(position);

            return true;
        }
    }
}
