using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    internal class AccessRepository(Context context) : IAccessRepository
    {
        public async Task<Access> GetByTelegramUserIdAsync(long telegramUserId)
        {
            var user = await context.Users
                .Include(x => x.Access)
                //.ThenInclude(x => x.VpnServer)
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return user?.Access;
        }

        public async Task DeleteAccessAsync(long telegramUserId)
        {
            var user = await context.Users
                .Include(x => x.Access)
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            context.Accesses.Remove(user.Access);

            await context.SaveChangesAsync();
        }
    }
}
