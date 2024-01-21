using Application.ControllerService.Common;
using Core.Common;
using Infrastructure.MailService;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Service.ControllerService.Service.Register
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            await CheckCaptchaTokenAsync(request.Token, settings?.CaptchaPrivateKey);

            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new ValidationException("Заполните обязательные поля");

            var user = await repositoryProvider.UserRepository.GetByLoginAsync(request.Login);

            if (user != null)
                throw new ValidationException("Пользователь с таким логином уже зарегистрирован");

            user = await repositoryProvider.UserRepository.GetByEmailAsync(request.Email);

            if (user != null)
                throw new ValidationException("Пользователь с таким адресом электронной почты уже зарегистрирован");

            //Добавляем пользователя
            var newUser = await repositoryProvider.UserRepository.AddAsync(new Core.Model.User.User()
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Role = Core.Model.User.UserRole.User,
                RegisterDate = DateTime.Now,
                Sost = UserSost.NotActive
            });

            //Добавляем активацию
            var guid = Guid.NewGuid();
            await repositoryProvider.ActiovationRepository.AddAsync(new Core.Model.User.Activation(0, newUser.Id, guid));

            var mailService = new MailService(repositoryProvider);
            await mailService.SendEmailAsync(request.Email, "Активация аккаунта", @$"
                Благодарим Вас, за регистрацию на нашем сервисе. 
                Для активации аккаунта передйите по <a href='https://lockvpn.me/activate?giud=${guid}'>ссылке</a>
            ");

            return true;
        }

        private async Task CheckCaptchaTokenAsync(string token, string privateKey)
        {
            if (string.IsNullOrEmpty(token))
                throw new ValidationException("Не удалось получить токен капчи");

            var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("secret", privateKey),
                new KeyValuePair<string, string>("response", token)
            ]);

            var response = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            var reponseString = await response.Content.ReadAsStringAsync();

            var result = bool.Parse(JObject.Parse(reponseString)["success"].ToString());

            if (!result)
                throw new ValidationException("Каптча не прошла проверку");
        }
    }
}
