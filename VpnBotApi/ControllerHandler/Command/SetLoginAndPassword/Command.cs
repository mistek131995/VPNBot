using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Command.SetLoginAndPassword
{
    public class Command : IControllerRequest<bool>
    {
        public long TelegramUserId { get; set; }
        public Guid AccessGuid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
