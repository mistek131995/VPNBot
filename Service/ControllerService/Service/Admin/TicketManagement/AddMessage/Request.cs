using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.TicketManagement.AddMessage
{
    public class Request : IRequest<bool>
    {
        public int TicketId { get; set; }
        public int UsertId { get; set; }
        public string Message { get; set; }
    }
}
