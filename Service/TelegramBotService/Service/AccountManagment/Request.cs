using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.AccountManagment
{
    public class Request(long telegramUserId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
    }
}
