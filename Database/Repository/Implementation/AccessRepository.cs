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
                .ThenInclude(x => x.VpnServer)
                .AsNoTracking()
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

        public async Task<List<Access>> GetDeprecatedAccessAsync(DateTime deprecatedDate)
        {
            return await context.Accesses
                .Where(x => x.EndDate.Date <= deprecatedDate.Date && x.IsDeprecated == false)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateManyAsync(List<Access> accessList)
        {
            context.Accesses.UpdateRange(accessList);

            await context.SaveChangesAsync();
        }
    }
}
