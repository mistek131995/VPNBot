using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.FreeExtend
{
    internal class Request(long telegramUserId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
    }
}
