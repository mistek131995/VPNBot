using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.Notification
{
    public class Request : IRequest<bool>
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public float amount { get; set; }
        public float in_amount { get; set; }
        public string? data { get; set; }
        public DateTime createdDateTime { get; set; }
        public string status { get; set; }
        public string? Signature { get; set; }
    }
}
