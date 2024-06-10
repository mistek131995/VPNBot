using Application.ControllerService.Common;
using Core.Common;
using Serilog;

namespace Service.ControllerService.Service.App.GetInitAppData
{
    internal class Service(IRepositoryProvider repositoryProvider, ILogger logger) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);

            result.IsExpired = user.AccessEndDate.Value.Date <= DateTime.Now.Date;
            result.AccessEndDate = user.AccessEndDate.Value.ToString("dd.MM.yyyy");

            var locations = await repositoryProvider.LocationRepository.GetAllAsync();

            result.Locations = locations
                .Where(x => x.VpnServers.Count > 0)
                .Select(x => new Result.Location()
                {
                    Id = x.Id,
                    Tag = x.Tag,
                    Name = x.Name,
                    Servers = x.VpnServers.Select(y => new Result.Server()
                    {
                        Ip = y.Ip,
                        Ping = 0
                    }).ToList()
                }).ToList();

            return result;
        }
    }
}
