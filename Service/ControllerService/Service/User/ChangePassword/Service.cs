using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangePassword
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден.");

            if (user.Password != request.OldPassword)
                throw new HandledException("Введен неверный пароль.");

            user.Password = request.Password;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
