using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.GetAccess
{
    public class Request(long telegramUserId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
    }
}
