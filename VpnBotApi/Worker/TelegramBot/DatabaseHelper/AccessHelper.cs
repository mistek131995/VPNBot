using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace VpnBotApi.Worker.TelegramBot.DatabaseHelper
{
    public class AccessHelper
    {
        public static async Task<List<Access>> GetAccessesByTelegramUserId(long telegramUserId, Context context)
        {
            var user = await context.Users
                .Include(x => x.Accesses.Where(x => x.EndDate > DateTime.Now))
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return user.Accesses.ToList();
        }
    }
}
