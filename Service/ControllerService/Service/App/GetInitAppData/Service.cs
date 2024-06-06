using Application.ControllerService.Common;
using Core.Common;
using IP2LocationIOComponent;

namespace Service.ControllerService.Service.App.GetInitAppData
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);

            Configuration Config = new()
            {
                ApiKey = "B402E5F8EAE840F9DF2AF3F76358F80E"
            };

            IPGeolocation IPL = new(Config);

            // Lookup ip address geolocation data
            var MyTask = await IPL.Lookup(request.Ip);
            var MyObj = MyTask;

            result.IpLocation = MyObj["country_code"]?.ToString() ?? "";

            if(user != null)
            {
                result.IsExpired = user.AccessEndDate.Value < DateTime.Now;
                result.AccessEndDate = user.AccessEndDate.Value.ToShortDateString();
            }

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
            }).ToList();

            return result;
        }
    }
}
