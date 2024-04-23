using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetProxyConnection
{
    public class Request : IRequest<Result>
    {
        public string Ip { get; set; }
        public int UserId { get; set; }
    }
}
