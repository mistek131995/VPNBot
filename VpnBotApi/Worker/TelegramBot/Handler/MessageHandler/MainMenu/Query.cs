using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.MainMenu
{
    public class Query(long telegramUserId, long telegramChatId) : IQuery<Response>
    {
        public long TelegramUserId = telegramUserId;
        public long TelegramChatId = telegramChatId;
    }
}
