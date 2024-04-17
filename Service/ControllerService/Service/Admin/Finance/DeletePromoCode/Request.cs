using Application.ControllerService.Common;

namespace Service.ControllerService.Service.Admin.Finance.DeletePromoCode
{
    public class Request : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
