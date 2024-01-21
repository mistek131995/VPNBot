using Application.ControllerService.Common;
using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Service.ControllerService.Service.Register
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            await CheckCaptchaTokenAsync(request.Token);

            if (string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new ValidationException("Заполните обязательные поля");

            var user = await repositoryProvider.UserRepository.GetByLoginAsync(request.Login);

            if (user != null)
                throw new ValidationException("Пользователь с таким логином уже зарегистрирован");

            user = await repositoryProvider.UserRepository.GetByEmailAsync(request.Email);

            if (user != null)
                throw new ValidationException("Пользователь с таким адресом электронной почты уже зарегистрирован");

            await repositoryProvider.UserRepository.AddAsync(new Core.Model.User.User()
            {
                Login = request.Login,
                Email = request.Email,
                Password = request.Password,
                Role = Core.Model.User.UserRole.User,
                RegisterDate = DateTime.Now,
                Sost = UserSost.NotActive
            });

            return true;
        }

        private async Task<bool> CheckCaptchaTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ValidationException("Каптча не прошла проверку");

            return true;
        }
    }
}
