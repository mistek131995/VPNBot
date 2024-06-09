using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.App.GetVpnLocation
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var locations = await repositoryProvider.LocationRepository.GetAllAsync();

            result.Locations = locations.Select(x => new Result.Location()
            {
                Id = x.Id,
                Tag = x.Tag,
                Name = x.Name,
                Servers = x.VpnServers.Select(y => new Result.Server()
                {
                    Ip = y.Ip,
                    Ping = 0
                }).ToList()
            })
            .ToList();

            return result;
        }
    }
}
