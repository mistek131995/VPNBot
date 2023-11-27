using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    internal class UserRepository(Context context) : IUserRepository
    {
        private readonly Context context = context;

        public async Task<User> AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByTelegramUserIdAsync(long telegramUserId)
        {
            return await context.Users
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);
        }

        public async Task<User> UpdateAsync(User user)
        {
            context.Update(user);

            await context.SaveChangesAsync();

            return user;
        }
    }
}
