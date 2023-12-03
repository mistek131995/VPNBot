using Application.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.GetQRCode
{
    public class Request(long telegramUserId) : IRequest<Result>
    {
        public long TelegramUserId = telegramUserId;
    }
}
