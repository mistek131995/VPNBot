using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Users.GetUser
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.Id) ??
                throw new HandledException("Пользователь не найден");

            return new Result()
            {
                Id = user.Id,
                Name = user.Login,
                Email = user.Email,
                Role = user.Role,
                AccessEndDate = user.AccessEndDate,
                Balance = user.Balance,
            };
        }
    }
}
