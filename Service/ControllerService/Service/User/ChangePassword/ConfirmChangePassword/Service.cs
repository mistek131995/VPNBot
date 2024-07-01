using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangePassword.ConfirmChangePassword
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByChangePasswordRequestGuidAsync(Guid.Parse(request.Guid))
                ?? throw new HandledException("Запрос на изменение пароля не найден");

            user.UpdatePassword(user.ChangePasswordRequest.Password);
            user.UpdateChangePasswordRequest(null);

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
