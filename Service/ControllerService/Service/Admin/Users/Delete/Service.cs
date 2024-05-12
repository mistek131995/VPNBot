using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Users.Delete
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден");

            await repositoryProvider.UserRepository.DeleteAsync(user);

            return true;
        }
    }
}
