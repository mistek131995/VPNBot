using Application.ControllerService.Common;
using Core.Common;
using Core.Model.Location;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.AddServer
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var location = await repositoryProvider.LocationRepository.GetByIdAsync(request.LocationId)
                ?? throw new HandledException("Страна не найдена");

            if (location.VpnServers.Any(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                throw new HandledException("Сервер с таким именем уже добавлен");

            if (location.VpnServers.Any(x => x.Ip.Trim() == request.IP.Trim()))
                throw new HandledException("Сервер с таким IP уже добавлен");

            location.VpnServers.Add(new VpnServer(0, request.IP, request.Name, request.Description, request.Port, request.UserName, request.Password, new List<ConnectionStatistic>()));
            await repositoryProvider.LocationRepository.UpdateAsync(location);

            return true;
        }
    }
}
