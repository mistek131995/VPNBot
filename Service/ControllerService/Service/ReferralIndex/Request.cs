using Application.ControllerService.Common;

namespace Service.ControllerService.Service.ReferralIndex
{
    public class Request : IRequest<Result>
    {
        public int UserId { get; set; }
    }
}
