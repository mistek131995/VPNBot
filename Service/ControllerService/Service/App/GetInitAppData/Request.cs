using Application.ControllerService.Common;

namespace Service.ControllerService.Service.App.GetInitAppData
{
    public class Request : IRequest<Result>
    {
        public int UsertId { get; set; }
        public string Ip {  get; set; }
    }
}
