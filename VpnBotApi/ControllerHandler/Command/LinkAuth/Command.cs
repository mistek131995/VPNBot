using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Command.LinkAuth
{
    public class Command : IControllerRequest<string>
    {
        public long TelegramUserId { get; set; }
        public Guid AccessGuid { get; set; }
        public string Password { get; set; }
    }
}
