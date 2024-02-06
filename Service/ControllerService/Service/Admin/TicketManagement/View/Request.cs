using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.TicketManagement.View
{
    public class Request : IRequest<Result>
    {
        public int TicketId { get; set; }
    }
}
