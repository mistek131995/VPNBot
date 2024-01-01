using Application.ControllerService.Common;

namespace Service.ControllerService.Service.AddLog
{
    public class Request : IRequest<bool>
    {
        public string Title { get; set; }
        public string StackTrace { get; set; }
    }
}
