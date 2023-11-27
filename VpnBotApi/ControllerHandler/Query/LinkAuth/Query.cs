using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Query.LinkAuth
{
    public class Query : IControllerRequest<Response>
    {
        public long TelegramUserId { get; set; }
        public Guid Guid { get; set; }
    }
}
