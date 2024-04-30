using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.CryptoCloud.Notification
{
    public class Request : IRequest<bool>
    {
        public string status { get; set; }
        public string invoice_id { get; set; }
        public string amount_crypto { get; set; }
        public string currency { get; set; }
        public Guid order_id { get; set; }
    }
}
