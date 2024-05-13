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

            position.Name = request.Name;
            position.Description = request.Description;
            position.Price = request.Price;
            position.GooglePlayIdentifier = request.GooglePlayIdentifier;
            position.MonthCount = request.MonthCount;

            await repositoryProvider.AccessPositionRepository.UpdateAsync(position);

            return true;
        }
    }
}
