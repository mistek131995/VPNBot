using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.PaymentMethod
{
    public class Handler : IHandler<Query, Response>
    {
        public Task<Response> HandlingAsync(Query query)
        {
            throw new NotImplementedException();
        }
    }
}
