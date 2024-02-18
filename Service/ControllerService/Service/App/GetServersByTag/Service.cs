using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.App.GetServersByTag
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var location = await repositoryProvider.LocationRepository.GetByTagAsync(request.Tag);

            var servers = location.VpnServers.Select(x => new Result.Server()
            {
                Tag = location.Tag,
                Ip = x.Ip,
                Ping = 0
            }).ToList();

            result.Servers.AddRange(servers);

            return result;
        }
    }
}
