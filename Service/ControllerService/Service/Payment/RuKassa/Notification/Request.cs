using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Request : IRequest<bool>
    {
        public string Query { get; set; }
        public string Signature { get; set; }
    }
}
