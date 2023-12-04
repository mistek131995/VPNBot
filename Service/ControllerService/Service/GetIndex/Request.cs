using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetIndex
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
