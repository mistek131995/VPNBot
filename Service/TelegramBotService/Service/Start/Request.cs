using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.Start
{
    public class Request(long telegramUserId, long telegramChatId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
        public long TelegramChatId = telegramChatId;
    }
}
