using Application.ControllerService.Common;

namespace Service.ControllerService.Service.UpdateByTag
{
    public class Request : IRequest<Result>
    {
        public string Tag { get; set; }
        public string Version { get; set; }
    }
}
