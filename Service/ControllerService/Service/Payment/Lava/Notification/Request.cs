using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.Lava.Notification
{
    public class Request : IRequest<bool>
    {
        public string invoice_id { get; set; }
        public string order_id { get; set; }
        public string status { get; set; }
        public string pay_time { get; set; }
        public decimal amount { get; set; }
        public decimal credited { get; set; }
        public string? Signature { get; set; }
    }
}
