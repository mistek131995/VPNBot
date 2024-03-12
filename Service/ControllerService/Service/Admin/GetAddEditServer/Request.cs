using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.GetAddEditServer
{
    public class Request : IRequest<Result>
    {
        public int ServerId { get; set; }
    }
}
