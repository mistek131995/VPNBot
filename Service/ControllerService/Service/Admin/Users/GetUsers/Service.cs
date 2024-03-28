using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Admin.Users.GetUsers
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        async Task<Result> IControllerService<Request, Result>.HandlingAsync(Request request)
        {
            var result = new Result();

            var users = await repositoryProvider.UserRepository.GetAllAsync();

            result.Count = users.Count;
            result.Users = users
                .Select(x => new Result.User(x.Id, x.Login, x.RegisterDate, x.AccessEndDate))
                .ToList();

            return result;
        }
    }
}
