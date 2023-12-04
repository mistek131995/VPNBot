using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.ChangePassword
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new Exception("Пользователь не найден.");

            if (user.Password != request.OldPassword)
                throw new Exception("Введен неверный пароль.");

            user.Password = request.Password;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
