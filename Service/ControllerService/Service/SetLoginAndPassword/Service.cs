using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.SetLoginAndPassword
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(request.TelegramUserId, request.AccessGuid)
            ?? throw new Exception("Пользователь не найден.");

            if (!string.IsNullOrEmpty(user.Login) && !string.IsNullOrEmpty(user.Password))
                throw new Exception("Логин и пароль уже установлены.");

            user.Login = request.Login;
            user.Password = request.Password;

            if (user.RegisterDate == DateTime.MinValue)
                user.RegisterDate = DateTime.Now;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
