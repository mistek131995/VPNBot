using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth;
using MD5Hash;
using Microsoft.Extensions.Configuration;
using Service.ControllerService.Common;
using Service.ControllerService.Common.Extensions;

namespace Service.ControllerService.Service.User.AuthWithGoogle
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            if (string.IsNullOrEmpty(request.Token) && string.IsNullOrEmpty(request.AcceptToken))
                throw new HandledException("Ошибка аутентификации");

            GoogleJsonWebSignature.Payload validPayload;

            if (string.IsNullOrEmpty(request.AcceptToken))
                validPayload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            else
                validPayload = await GoogleAuth.ValidateAccessToken(request.AcceptToken);

            var user = await repositoryProvider.UserRepository.GetByEmailAsync(validPayload.Email);

            if (user == null)
            {
                var login = validPayload.Email.Split('@')[0];

                var userWithLogin = await repositoryProvider.UserRepository.GetByLoginAsync(login);

                while (userWithLogin != null)
                {
                    login = login + new Random().Next(0, 9);
                    userWithLogin = await repositoryProvider.UserRepository.GetByLoginAsync(login);
                }

                var password = string.Join("", validPayload.Email.GetMD5().Take(8)).GetMD5();

                user = await repositoryProvider.UserRepository.AddAsync(new Core.Model.User.User(login, validPayload.Email, password, UserSost.Active));
            }

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
