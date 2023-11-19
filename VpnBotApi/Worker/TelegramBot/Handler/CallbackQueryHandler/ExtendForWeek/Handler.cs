using Database.Common;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.ExtendForWeek
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider provider = provider;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var access = await provider.AccessRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            if(access == null) { }

            return response;
        }
    }
}