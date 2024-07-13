using Application.ControllerService.Common;
using Microsoft.AspNetCore.Http;

namespace Service.ControllerService.Service.Ticket.AddMessage
{
    public class Request : IRequest<bool>
    {
        public int TicketId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }

        public IFormFileCollection? FormFiles { get; set; }
    }
}
