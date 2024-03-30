using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.Lava.CreateLink
{
    public class Request : IRequest<string>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
