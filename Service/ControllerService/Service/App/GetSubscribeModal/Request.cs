using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetSubscribeModal
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
