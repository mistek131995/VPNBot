using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.GetStatistics
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            //Пользователи за вчера и сегодня
            var users = await repositoryProvider.UserRepository.GetAllAsync();

            result.RegisterToday = users.Where(x => x.RegisterDate.Date == DateTime.Now.Date).Count();
            result.RegisterYesterday = users.Where(x => x.RegisterDate.Date == DateTime.Now.Date.AddDays(-1)).Count();
            result.TotalUsers = users.Count();

            var locations = await repositoryProvider.LocationRepository.GetAllAsync();
            result.ConnectionByLocations = locations.Where(x => x.VpnServers.Count > 0)
                .Select(x => new Result.ConnectionByLocation(
                    x.Name,
                    x.VpnServers.SelectMany(s => s.Statistics).Where(s => s.Date.Date == DateTime.Now.Date).Sum(s => s.Count),
                    x.VpnServers.SelectMany(s => s.Statistics).Where(s => s.Date.Date == DateTime.Now.AddDays(-1).Date).Sum(s => s.Count),
                    x.VpnServers.SelectMany(s => s.Statistics).Sum(s => s.Count)))
                .ToList();

            return result;
        }
    }
}
