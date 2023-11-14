using Database;
using VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.DatabaseHelper;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Service
{
    public class AccessService
    {
        public async Task GetAccess(long telegramUserId, Context context)
        {
            var accesses = await AccessHelper.GetAccessByTelegramUserId(telegramUserId, context);


        }

        public async Task CreateNewAccess()
        {

        }
    }
}
