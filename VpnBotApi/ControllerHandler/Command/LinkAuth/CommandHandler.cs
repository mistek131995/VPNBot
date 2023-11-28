using Database.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VpnBotApi.Common;
using static VpnBotApi.Program;

namespace VpnBotApi.ControllerHandler.Command.LinkAuth
{
    public class CommandHandler(IRepositoryProvider repositoryProvider) : IControllerHandler<Command, string>
    {
        public async Task<string> HandlingAsync(Command query)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(query.TelegramUserId, query.AccessGuid) 
                ?? throw new Exception("Пользователь не найден.");

            if (user.Password != query.Password)
                throw new Exception("Введен неверный логин или пароль.");

            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("login", user.Login),
                new Claim("role", user.Role.ToString())
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)), // время действия 1 день
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
