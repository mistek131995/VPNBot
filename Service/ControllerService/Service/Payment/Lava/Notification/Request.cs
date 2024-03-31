using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.Lava.Notification
{
    public class Request : IRequest<bool>
    {
        public string Signature { get; set; }
    }
}
