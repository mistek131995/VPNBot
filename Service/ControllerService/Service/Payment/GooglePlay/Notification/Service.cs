using Application.ControllerService.Common;
using Core.Common;
using System.Text;

namespace Service.ControllerService.Service.Payment.GooglePlay.Notification
{
    internal class Service(IRepositoryProvider repositoryProvider, Serilog.ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var base64EncodedBytes = Convert.FromBase64String(request.message.data);
            logger.Information(Encoding.UTF8.GetString(base64EncodedBytes));

            return true;
        }
    }
}
