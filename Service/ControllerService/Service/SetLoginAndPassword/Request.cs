using Application.ControllerService.Common;

namespace Service.ControllerService.Service.SetLoginAndPassword
{
    public class Request : IRequest<bool>
    {
        public long TelegramUserId { get; set; }
        public Guid AccessGuid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
