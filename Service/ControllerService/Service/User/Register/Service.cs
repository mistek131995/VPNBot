using Application.ControllerService.Common;
using Core.Common;
using Core.Model.User;
using Infrastructure.MailService;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.Register
{
    public class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            if (!string.IsNullOrEmpty(request.Token))
                await Helper.CheckCaptchaTokenAsync(request.Token, settings.CaptchaPrivateKey);

            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new HandledException("Заполните обязательные поля");

            if(!request.Email.Contains("@"))
                throw new HandledException("Неверный формат электронной почты");

            var user = await repositoryProvider.UserRepository.GetByLoginAsync(request.Login.Trim().ToLower());

            if (user != null)
                throw new HandledException("Пользователь с таким логином уже зарегистрирован");

            user = await repositoryProvider.UserRepository.GetByEmailAsync(request.Email.Trim().ToLower());

            if (user != null)
                throw new HandledException("Пользователь с таким адресом электронной почты уже зарегистрирован");

            //Реферальная программа
            var parentId = 0;
            if (request.Guid != null)
            {
                var parentUser = await repositoryProvider.UserRepository.GetByGuidAsync(request.Guid ?? new Guid())
                    ?? throw new HandledException("Не удалось найти пользователя для привязки реферальной программы.");

                parentId = parentUser.Id;
            }


            var newUser = new Core.Model.User.User(request.Login, request.Email, request.Password, UserSost.NotActive, new UserSetting(false, false, false), parentId);

            //Добавляем пользователя
            newUser = await repositoryProvider.UserRepository.AddAsync(newUser);

            //Добавляем активацию
            var guid = Guid.NewGuid();
            await repositoryProvider.ActivationRepository.AddAsync(new Core.Model.User.Activation(0, newUser.Id, guid));

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(request.Email, "Активация аккаунта", @$"
                Благодарим Вас, за регистрацию на нашем сервисе. 
                Для активации аккаунта перейдите по <a href='https://{configuration["Domain"]}/Activation?guid={guid}'>ссылке</a>
            ");

            return true;
        }
    }
}
