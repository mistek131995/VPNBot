using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;
using Core.Model.User;
using Infrastructure.MailService;
using Microsoft.Extensions.Configuration;

namespace Service.ControllerService.Service.User.ChangeEmail.AddChangeEmailRequest
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UsertId) 
                ?? throw new HandledException("Пользователь не найден");

            user.ChangeEmailRequest = new ChangeEmailRequest(Guid.NewGuid(), request.Email);

            await repositoryProvider.UserRepository.UpdateAsync(user);


            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(user.Email, "Смена пароля", @$"Для вашего аккаунта, был создан запрос на смену эектронной почты. \n
                Для подтверждения смены Электронной почты перейдите по  <a href='https://{configuration["Domain"]}?confirm-change-email-request-guid={user.ChangePasswordRequest.Guid}'>ссылке</a>. \n
                Если вы не создавали запрос на смену электронной почты, перейдите в аккаунт и обновите пароль.");

            return true;

        }
    }
}
