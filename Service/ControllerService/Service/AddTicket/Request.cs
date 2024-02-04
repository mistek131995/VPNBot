using Application.ControllerService.Common;

namespace Service.ControllerService.Service.AddTicket
{
    public class Request : IRequest<int>
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
