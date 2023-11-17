using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess
{
    public class Query(long telegramUserId) : IQuery<Response>
    {
        public long TelegramUserId = telegramUserId;
    }
}
