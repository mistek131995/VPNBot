using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth;
using MD5Hash;
using Microsoft.Extensions.Configuration;
using Core.Model.User;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.AuthWithGoogle
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(request.Token);

            var user = await repositoryProvider.UserRepository.GetByEmailAsync(validPayload.Email);

            if (user == null)
            {
                var login = validPayload.Email.Split('@')[0];
                var password = string.Join("", validPayload.Email.GetMD5().Take(8)).GetMD5();

                user = await repositoryProvider.UserRepository.AddAsync(new Core.Model.User.User()
                {
                    Login = login,
                    Email = validPayload.Email,
                    Password = password,
                    Role = UserRole.User,
                    AccessEndDate = DateTime.Now.AddDays(7),
                    Sost = UserSost.Active,
                    Balance = 0,
                    Guid = Guid.NewGuid(),
                    RegisterDate = DateTime.Now
                });
            }

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
