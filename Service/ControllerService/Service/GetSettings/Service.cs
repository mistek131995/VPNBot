using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetSettings
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            return new Result()
            {
                CaptchaPublicKey = settings.CaptchaPublicKey
            };
        }
    }
}
