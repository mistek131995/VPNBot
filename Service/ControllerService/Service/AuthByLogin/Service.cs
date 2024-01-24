using Application.ControllerService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.AuthByLogin
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {

            if (!string.IsNullOrEmpty(request.Token))
            {
                var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();
                await Helper.CheckCaptchaTokenAsync(request.Token, settings?.CaptchaPrivateKey);
            }

            var user = await repositoryProvider.UserRepository.GetByLoginAndPasswordAsync(request.Login, request.Password) ??
                throw new Exception("Пользователь с таким логином и паролем не найден.");

            if(user.Sost == UserSost.Blocked)
                throw new Exception("Ваш аккаунт заблокирован.");

            if (user.Sost == UserSost.NotActive)
                throw new Exception("Ваш аккаунт не активирован. Проверьте электронную почту, письмо может быть в папке 'Спам'.");

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
