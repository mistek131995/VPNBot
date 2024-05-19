using Application.ControllerService.Common;
using Core.Common;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.Auth.OAuth2;
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

            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "354549970311-bi5ordl9jsbsbl874vob4bq1ahr6r98k.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                new[] { AndroidPublisherService.Scope.Androidpublisher },
                "user",
                CancellationToken.None);

            //var response = await httpClient.GetAsync($"https://androidpublisher.googleapis.com/androidpublisher/v3/applications/com.lockvpnandroidapp/purchases/subscriptionsv2/tokens/{request.SubscribeToken}");
            //response.EnsureSuccessStatusCode();
            //var data = await response.Content.ReadAsStringAsync();

            logger.Information(credential.Token.AccessToken);

            return true;
        }
    }
}
