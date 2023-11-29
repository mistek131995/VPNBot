using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.BuyAccess
{
    public class Query(int accessPositionId, long telegramUserId) : IQuery<Response>
    {
        public int AccessPositionId = accessPositionId;
        public long TelegramUserId = telegramUserId;
    }
}
