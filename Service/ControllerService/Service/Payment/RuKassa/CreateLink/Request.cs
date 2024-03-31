using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.RuKassa.CreateLink
{
    public class Request : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
