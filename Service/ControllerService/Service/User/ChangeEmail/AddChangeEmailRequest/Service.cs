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
            if (!request.Email.Contains("@"))
                throw new HandledException("Неверный формат электронной почты");

            var user = await repositoryProvider.UserRepository.GetByEmailAsync(request.Email);

            if(user != null) 
                throw new HandledException("Адрес электронной почты уже используется");

            user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) 
                ?? throw new HandledException("Пользователь не найден");

            user.ChangeEmailRequest = new ChangeEmailRequest(Guid.NewGuid(), request.Email);

            await repositoryProvider.UserRepository.UpdateAsync(user);


            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(user.Email, "Смена электронной почты", @$"Для вашего аккаунта, был создан запрос на смену эектронной почты.
                Если вы не создавали запрос на смену электронной почты, перейдите в аккаунт и обновите пароль.");

            await mailService.SendEmailAsync(request.Email, "Смена электронной почты", @$"Для вашего аккаунта, был создан запрос на смену эектронной почты.
                Для подтверждения смены Электронной почты перейдите по <a href='https://{configuration["Domain"]}?confirm-change-email-request-guid={user.ChangeEmailRequest.Guid}'>ссылке</a>.");

            return true;

        }
    }
}
