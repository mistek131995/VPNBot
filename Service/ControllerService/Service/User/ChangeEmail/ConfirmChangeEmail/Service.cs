using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangeEmail.ConfirmChangeEmail
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByChangeEmailRequestGuidAsync(Guid.Parse(request.Guid))
                ?? throw new HandledException("Запрос на изменение электронной почты не найден");

            user.UpdateEmail(user.ChangeEmailRequest.Email);
            user.UpdateChangeEmailRequest(null);

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
