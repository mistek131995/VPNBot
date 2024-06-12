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
            var users = await repositoryProvider.UserRepository.GetByRegisterDateRange(DateTime.Now.AddDays(-1), DateTime.Now);

            result.RegisterToday = users.Where(x => x.RegisterDate.Date == DateTime.Now.Date).Count();
            result.RegisterTomorrow = users.Where(x => x.RegisterDate.Date == DateTime.Now.Date.AddDays(-1)).Count();

            return result;
        }
    }
}
