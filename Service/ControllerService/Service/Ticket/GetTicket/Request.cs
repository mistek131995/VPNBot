using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Ticket.GetTicket
{
    public class Request : IRequest<Result>
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
