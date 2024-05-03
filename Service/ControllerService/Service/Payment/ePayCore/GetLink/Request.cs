using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.ePayCore.GetLink
{
    public class Request : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string? PromoCode { get; set; }
    }
}
