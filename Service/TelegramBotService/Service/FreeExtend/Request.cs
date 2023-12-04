using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.FreeExtend
{
    public class Request(long telegramUserId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
    }
}
