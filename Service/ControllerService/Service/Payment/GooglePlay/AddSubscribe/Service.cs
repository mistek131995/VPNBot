using Application.ControllerService.Common;
using Core.Common;
using Newtonsoft.Json.Linq;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.GooglePlay.AddSubscribe
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) 
                ?? throw new HandledException("Пользователь не найден", true);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/com.lockvpnandroidapp/purchases/subscriptionsv2/tokens/{request.SubscribeToken}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();

            logger.Information(data);

            return true;
        }
    }
}
