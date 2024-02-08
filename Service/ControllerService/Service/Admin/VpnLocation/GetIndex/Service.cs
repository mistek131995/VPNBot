using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.VpnLocation.GetIndex
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
                Name = x.Name,
                Servers = x.VpnServers.Select(s => new Result.Server()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Ip = s.Ip,
                    Port = s.Port,
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
