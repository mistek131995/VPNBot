using Application.ControllerService.Common;

namespace Service.ControllerService.Service.ExtendSubscribeForBonuses
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }
    }
}
