using Application.ControllerService.Common;
using Core.Model.Ticket;

namespace Service.ControllerService.Service.Admin.TicketManagement.UpdateCondition
{
    public class Request : IRequest<bool>
    {
        public int TicketId { get; set; }
        public TicketCondition Condition { get; set; }
    }
}
