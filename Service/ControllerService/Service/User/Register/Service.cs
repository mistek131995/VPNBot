using Application.ControllerService.Common;
using Core.Common;
using Infrastructure.MailService;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.Register
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            if (!string.IsNullOrEmpty(request.Token))
                await Helper.CheckCaptchaTokenAsync(request.Token, settings.CaptchaPrivateKey);

            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new HandledExeption("Заполните обязательные поля");

            var user = await repositoryProvider.UserRepository.GetByLoginAsync(request.Login.Trim().ToLower());

            if (user != null)
                throw new HandledExeption("Пользователь с таким логином уже зарегистрирован");

            user = await repositoryProvider.UserRepository.GetByEmailAsync(request.Email.Trim().ToLower());

            if (user != null)
                throw new HandledExeption("Пользователь с таким адресом электронной почты уже зарегистрирован");

            var newUser = new Core.Model.User.User()
            {
                Login = request.Login.Trim().ToLower(),
                Email = request.Email.Trim().ToLower(),
                Password = request.Password,
                Role = Core.Model.User.UserRole.User,
                RegisterDate = DateTime.Now,
                Sost = UserSost.NotActive,
                Guid = Guid.NewGuid(),
            };

            if (request.Guid != null)
            {
                var parentUser = await repositoryProvider.UserRepository.GetByGuidAsync(request.Guid ?? new Guid())
                    ?? throw new HandledExeption("Не удалось найти пользователя для привязки реферальной программы.");

                newUser.ParentUserId = parentUser.Id;
            }

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
