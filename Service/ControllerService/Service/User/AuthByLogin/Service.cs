using Application.ControllerService.Common;
using Core.Common;
using Infrastructure.MailService;
using Infrastructure.TelegramService;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.AuthByLogin
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration, LoginInFailureService loginInFailureService, MailService mailService, ITelegramNotificationService telegramNotificationService) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            loginInFailureService.CheckLoginInRequest(request.Ip);

            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            var user = await repositoryProvider.UserRepository.GetByLoginAndPasswordAsync(request.Login.Trim().ToLower(), request.Password);

            if(user == null)
            {
                loginInFailureService.AddFailure(request.Ip, user?.Id ?? 0);

                user = await repositoryProvider.UserRepository.GetByLoginAsync(request.Login);

                if(user != null)
                {
                    if(user.UserSetting.UseEmailNotificationLoginInError)
                        await mailService.SendEmailAsync(user.Email, "Попытка входа в аккаунт", "Была обнаружена неудачная попытка входа в Ваш аккаунт. Если это не Вы, рекомендуем сменить пароль.");
                    if (user.UserSetting.UseTelegramNotificationLoginInError)
                        await telegramNotificationService.AddText("Была обнаружена неудачная попытка входа в Ваш аккаунт. Если это не Вы, рекомендуем сменить пароль.").SendNotificationAsync(user.TelegramUserId);
                }

                throw new HandledException("Пользователь с таким логином и паролем не найден.");
            }

            if (user.Sost == UserSost.Blocked)
                throw new HandledException("Ваш аккаунт заблокирован.");

            if (user.Sost == UserSost.NotActive)
                throw new HandledException("Ваш аккаунт не активирован. Проверьте электронную почту, письмо может быть в папке 'Спам'.");

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
