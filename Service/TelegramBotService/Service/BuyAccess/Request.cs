using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.BuyAccess
{
    public class Request(long telegramUserrId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserrId;
    }
}
