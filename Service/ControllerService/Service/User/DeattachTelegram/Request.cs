using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.DeattachTelegram
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
