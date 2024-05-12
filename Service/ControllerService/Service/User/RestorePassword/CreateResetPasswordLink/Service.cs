using Application.ControllerService.Common;
using Core.Common;
using Infrastructure.MailService;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.RestorePassword.CreateResetPasswordLink
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();
            await Helper.CheckCaptchaTokenAsync(request.Token, settings.CaptchaPrivateKey);

            var user = await repositoryProvider.UserRepository.GetByEmailAsync(request.Email) ??
                throw new HandledException("Пользователь с таким адресом электронной почты не найден.");

            var resetPassword = await repositoryProvider.ResetPasswordRepository.GetByUserIdAsync(user.Id);

            //Если ссылка уже существует, возвращаем ее
            if (resetPassword != null)
            {
                await SendResetPasswordEmail(user.Email, resetPassword.Guid);
                return true;
            }


            resetPassword = new Core.Model.User.ResetPassword()
            {
                UserId = user.Id,
                Guid = Guid.NewGuid(),
            };

            await repositoryProvider.ResetPasswordRepository.AddAsync(resetPassword);
            await SendResetPasswordEmail(user.Email, resetPassword.Guid);

            return true;
        }

        private async Task SendResetPasswordEmail(string email, Guid guid)
        {
            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(email, "Сброс пароля", @$"
                Для сброса пароля, перейдите по <a href='https://{configuration["Domain"]}/reset-password?guid={guid}'>ссылке</a>
            ");
        }
    }
}
