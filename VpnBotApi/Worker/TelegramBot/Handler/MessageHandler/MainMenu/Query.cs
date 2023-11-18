using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.MainMenu
{
    public class Query(long telegramUserId) : IQuery<Response>
    {
        public long TelegramUserId = telegramUserId;
    }
}
