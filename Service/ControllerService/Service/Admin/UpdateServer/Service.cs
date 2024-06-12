using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.UpdateServer
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var location = await repositoryProvider.LocationRepository.GetByServerIdAsync(request.Id) 
                ?? throw new HandledException("Сервер не найден");
            var server = location.VpnServers.FirstOrDefault(x => x.Id == request.Id) 
                ?? throw new HandledException("Сервер не найден");

            if(location.Id != request.LocationId)
            {
                var newServerLocation = await repositoryProvider.LocationRepository.GetByIdAsync(request.LocationId) 
                    ?? throw new HandledException("Локация не найдена");
                newServerLocation.VpnServers.Add(new Core.Model.Location.VpnServer(
                    0, 
                    request.Ip, 
                    request.Name, 
                    request.Description, 
                    request.Port, 
                    request.UserName, 
                    request.Password, 
                    new List<Core.Model.Location.ConnectionStatistic>()));

                await repositoryProvider.LocationRepository.UpdateAsync(newServerLocation);

                location.VpnServers.Remove(server);
            }
            else
            {
                server.Name = request.Name;
                server.Description = request.Description;
                server.Ip = request.Ip;
                server.Port = request.Port;
                server.UserName = request.UserName;
                server.Password = request.Password;
            }

            await repositoryProvider.LocationRepository.UpdateAsync(location);

            return true;
        }
    }
}
