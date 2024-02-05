using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Ticket.GetUserTickets
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
