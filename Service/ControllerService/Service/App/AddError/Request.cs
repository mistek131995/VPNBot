using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.AddError
{
    public class Request : IRequest<bool>
    {
        public string? Location { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Ip { get; set; }
    }
}
