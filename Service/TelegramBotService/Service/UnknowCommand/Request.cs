using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.UnknowCommand
{
    public class Request(long telegramUserId, long telegramChatId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
        public long TelegramChatId = telegramChatId;
    }
}
