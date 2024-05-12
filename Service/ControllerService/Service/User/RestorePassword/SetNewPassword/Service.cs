using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.RestorePassword.SetNewPassword
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var resetPassword = await repositoryProvider.ResetPasswordRepository.GetByGuidAsync(request.Guid)
                ?? throw new HandledException("Некорректная ссылка на сброс пароля, запросите новую ссылку");

            var user = await repositoryProvider.UserRepository.GetByIdAsync(resetPassword.UserId) 
                ?? throw new HandledException("Пользователь не найден");

            user.Password = request.Password;
            await repositoryProvider.UserRepository.UpdateAsync(user);

            await repositoryProvider.ResetPasswordRepository.DeleteAsync(request.Guid);

            return true;
        }
    }
}
