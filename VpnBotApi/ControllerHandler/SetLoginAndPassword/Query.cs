using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.SetLoginAndPassword
{
    public class Query : IControllerQuery<bool>
    {
        public long TelegramUserId { get; set; }
        public Guid AccessGuid { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
    }
}
