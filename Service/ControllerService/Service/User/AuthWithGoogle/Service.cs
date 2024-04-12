using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace Service.ControllerService.Service.User.AuthWithGoogle
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, string>
    {
        public async Task<string> HandlingAsync(Request request)
        {
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(request.Token);

            Console.WriteLine(validPayload.Email);

            return "";
        }
    }
}
