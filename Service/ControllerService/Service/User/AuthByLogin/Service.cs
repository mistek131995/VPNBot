using Application.ControllerService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.AuthByLogin
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            await Helper.CheckCaptchaTokenAsync(request.Token, settings.CaptchaPrivateKey);

            var user = await repositoryProvider.UserRepository.GetByLoginAndPasswordAsync(request.Login.Trim().ToLower(), request.Password) ??
                throw new HandledException("Пользователь с таким логином и паролем не найден.");

            if (user.Sost == UserSost.Blocked)
                throw new HandledException("Ваш аккаунт заблокирован.");

            if (user.Sost == UserSost.NotActive)
                throw new HandledException("Ваш аккаунт не активирован. Проверьте электронную почту, письмо может быть в папке 'Спам'.");

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
