using Application.ControllerService.Common;

namespace Service.ControllerService.Service.AuthByLink
{
    public class Request : IRequest<string>
    {
        public long TelegramUserId { get; set; }
        public Guid AccessGuid { get; set; }
        public string Password { get; set; }
    }
}
