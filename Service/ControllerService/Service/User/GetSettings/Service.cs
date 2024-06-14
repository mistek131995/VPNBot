using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.GetSettings
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UsertId) 
                ?? throw new HandledException("Пользователь не найден");

            result.Email = user.Email;

            return result;
        }
    }
}
