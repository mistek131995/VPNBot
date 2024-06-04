using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetInitAppData
{
    public class Request : IRequest<Result>
    {
        public string Ip {  get; set; }
    }
}
