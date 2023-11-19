using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.SubscribeManagement
{
    public class Query(long telegramUserId) : IQuery<Response>
    {
        public long TelegramUserId = telegramUserId;
    }
}
