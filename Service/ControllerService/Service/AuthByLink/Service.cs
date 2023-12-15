using Application.ControllerService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.ControllerService.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.ControllerService.Service.AuthByLink
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(request.TelegramUserId, request.AccessGuid)
                ?? throw new Exception("Пользователь не найден.");

            if (user.Password != request.Password)
                throw new Exception("Введен неверный логин или пароль.");

            if (user.Role != Core.Model.User.UserRole.Admin)
                throw new Exception("Личный кабинет находится в разработке, пока в него нельзя попасть.");

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
