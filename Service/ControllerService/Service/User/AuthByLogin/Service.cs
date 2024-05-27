using Application.ControllerService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.AuthByLogin
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration, LoginInFailureService loginInFailureService) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            loginInFailureService.CheckLoginInRequest(request.Ip);

            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            var user = await repositoryProvider.UserRepository.GetByLoginAndPasswordAsync(request.Login.Trim().ToLower(), request.Password);

            if(user == null)
            {
                loginInFailureService.AddFailure(request.Ip, user?.Id ?? 0);
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
