using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.DatabaseHelper
{
    public class AccessHelper
    {
        public static async Task<List<Access>> GetAccessByTelegramUserId(long telegramUserId, Context context)
        {
            var user = await context.Users
                .Include(x => x.Accesses)
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return user.Accesses.ToList();
        }
    }
}
