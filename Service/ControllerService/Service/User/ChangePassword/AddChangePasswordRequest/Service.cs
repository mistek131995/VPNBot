using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.MailService;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.ChangePassword.AddChangePasswordRequest
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден");

            user.ChangePasswordRequest = new ChangePasswordRequest(Guid.NewGuid(), request.Password);
            await repositoryProvider.UserRepository.UpdateAsync(user);

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(user.Email, "Смена пароля", @$"Для вашего аккаунта, был создан запрос на смену пароля.</br>
                Для подтверждения смены пароля перейдите по  <a href='https://{configuration["Domain"]}/confirm-change-password-request?guid={user.ChangePasswordRequest.Guid}'>ссылке</a>.</br>
                Если вы не создавали запрос на смену пароля, перейдите в аккаунт и обновите пароль.");

            return true;
        }
    }
}
