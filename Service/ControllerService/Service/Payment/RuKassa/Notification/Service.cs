using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            return true;
        }
    }
}
