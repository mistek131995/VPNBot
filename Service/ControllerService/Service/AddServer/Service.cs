using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using Core.Model.VpnServer;

namespace Service.ControllerService.Service.AddServer
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var server = await repositoryProvider.VpnServerRepository.GetAllAsync();

            if (server.Any(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower()))
                throw new HandledExeption("Сервер с таким именем уже добавлен");

            if (server.Any(x => x.Ip.Trim() == request.IP.Trim()))
                throw new HandledExeption("Сервер с таким IP уже добавлен");

            await repositoryProvider.VpnServerRepository.AddAsync(new VpnServer(0, request.IP, request.Name, request.Description, request.Port, 0, request.UserName, request.Password, request.CountryId));

            return true;
        }
    }
}
