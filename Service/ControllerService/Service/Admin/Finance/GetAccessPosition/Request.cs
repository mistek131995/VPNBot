using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.GetAccessPosition
{
    public class Request : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
