using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Location.DeleteServer
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var location = await repositoryProvider.LocationRepository.GetByServerIdAsync(request.Id)
                ?? throw new HandledException("Сервер не найден");

            location.DeleteServer(request.Id);

            await repositoryProvider.LocationRepository.UpdateAsync(location);

            return true;
        }
    }
}
