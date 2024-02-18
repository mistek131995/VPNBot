using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetServersByTag
{
    public class Request : IRequest<Result>
    {
        public string Tag { get; set; }
    }
}
