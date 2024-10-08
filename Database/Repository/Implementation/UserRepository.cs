﻿using Database.Model;
using Database.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository.Implementation
{
    internal class UserRepository(Context context) : IUserRepository
    {
        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users
                .Include(x => x.Access)
                .Include(x => x.Payments)
                .ThenInclude(x => x.AccessPosition)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByTelegramUserIdAndAccessGuidAsync(long telegramUserId, Guid accessGuid)
        {
            return await context.Users
                .Include(x => x.Access)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId && x.Access.Guid == accessGuid);
        }

        public async Task<User> GetByTelegramUserIdAsync(long telegramUserId)
        {
            return await context.Users
                .Include(x => x.Access)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TelegramUserId == telegramUserId);
        }

        public async Task<User> AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {

            if(user.Access != null && user.Access.Id == 0)
            {
                var oldAccess = await context.Accesses.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (oldAccess != null)
                {
                    context.Accesses.Remove(oldAccess);
                }
            }


            context.Update(user);

            await context.SaveChangesAsync();

            return user;
        }
    }
}
