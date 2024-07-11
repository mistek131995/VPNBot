using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.SaveTelegramId
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
        public long TelegramId { get; set; }
    }
}
