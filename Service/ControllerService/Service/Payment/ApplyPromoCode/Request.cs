using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.ApplyPromoCode
{
    public class Request : IRequest<int>
    {
        public int UserId { get; set; }
        public string PromoCode { get; set; }
        public int PositionId { get; set; }
    }
}
