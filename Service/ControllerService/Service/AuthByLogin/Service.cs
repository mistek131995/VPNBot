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
            var user = await repositoryProvider.UserRepository.GetByLoginAndPasswordAsync(request.Login, request.Password) ??
                throw new Exception("Пользователь с таким логином и паролем не найден.");

            return Helper.CreateJwtToken(user, configuration);
        }
    }
}
