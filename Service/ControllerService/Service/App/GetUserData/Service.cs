using Application.ControllerService.Common;
using Core.Common;
using IP2LocationIOComponent;

namespace Service.ControllerService.Service.App.GetUserData
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);

            result.AccessIsExpire = user.AccessEndDate?.Date <= DateTime.Now.Date;

            if (result.AccessIsExpire)
            {
                Configuration Config = new()
                {
                    ApiKey = "B402E5F8EAE840F9DF2AF3F76358F80E"
                };

                IPGeolocation IPL = new(Config);

                // Lookup ip address geolocation data
                var MyTask = await IPL.Lookup(request.Ip);
                var MyObj = MyTask;

                result.IpLocation = MyObj["country_code"]?.ToString() ?? "";
            }

            return result;
        }
    }
}
