using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    internal class AccessRepository(Context context) : IAccessRepository
    {
        private readonly Context context = context;

        public async Task<Access> GetByTelegramUserIdAsync(long telegramUserId)
        {
            var user = await context.Users.AsNoTracking()
                .Include(x => x.Access)
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);

            return user?.Access;
        }
    }
}
