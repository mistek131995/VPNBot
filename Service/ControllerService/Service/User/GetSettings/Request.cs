using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.GetSettings
{
    public class Request : IRequest<Result>
    {
        public int UsertId { get; set; }
    }
}
