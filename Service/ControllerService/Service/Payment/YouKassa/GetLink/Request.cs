using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.YouKassa.GetLink
{
    public class Request : IRequest<string>
    {
        public int UserId { get; set; }
        public int Id { get; set; }

        public string? PromoCode { get; set; }
    }
}
