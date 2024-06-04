using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetConnectionByIP
{
    public class Request : IRequest<Result>
    {
        public string Ip { get; set; }
        public string OS { get; set; }
        public int UserId { get; set; }
    }
}
