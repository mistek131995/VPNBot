using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetAccessPositions
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
