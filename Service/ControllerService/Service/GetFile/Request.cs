using Application.ControllerService.Common;

namespace Service.ControllerService.Service.GetFile
{
    public class Request : IRequest<Result>
    {
        public string Tag { get; set; }
    }
}
