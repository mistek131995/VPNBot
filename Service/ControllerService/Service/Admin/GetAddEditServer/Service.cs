using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.GetAddEditServer
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var location = await repositoryProvider.LocationRepository.GetByServerIdAsync(request.ServerId)
                ?? throw new HandledExeption("Сервер не найден");
            var server = location.VpnServers.FirstOrDefault(x => x.Id == request.ServerId)
                ?? throw new HandledExeption("Сервер не найден");


            var countries = await repositoryProvider.LocationRepository.GetAllAsync();

            result.Name = server.Name;
            result.Description = server.Description;
            result.Ip = server.Ip;
            result.Port = server.Port;
            result.UserName = server.UserName;
            result.Password = server.Password;
            result.CountryId = location.Id;
            result.Countries = countries
                .Select(x => new Result.Option(x.Id.ToString(), x.Name))
                .ToList();

            return result;
        }
    }
}
