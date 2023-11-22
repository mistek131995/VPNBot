using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Auth
{
    public class Query : IControllerQuery<Response>
    {
        public long TelegramUserId { get; set; }
        public Guid Guid { get; set; }
    }
}
