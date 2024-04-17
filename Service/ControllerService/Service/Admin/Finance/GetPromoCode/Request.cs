using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.GetPromoCode
{
    public class Request : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
