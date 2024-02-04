using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetUserTickets
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
