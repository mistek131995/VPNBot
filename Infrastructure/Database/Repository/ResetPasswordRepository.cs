using Core.Model.User;
using Core.Repository;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Database.Repository
{
    internal class ResetPasswordRepository(ContextFactory context) : IResetPasswordRepository
    {
        public async Task<Guid> AddAsync(ResetPassword model)
        {
            context.ResetPasswords.Add(new Entity.ResetPassword()
            {
                UserId = model.UserId,
                Guid = model.Guid
            });
            await context.SaveChangesAsync();

            return model.Guid;
        }

        public async Task DeleteAsync(Guid guid)
        {
            var resetPassword = await context.ResetPasswords.FirstOrDefaultAsync(x => x.Guid == guid);

            if (resetPassword != null)
            {
                context.ResetPasswords.Remove(resetPassword);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ResetPassword> GetByIdAsync(int id)
        {
            var resetPassword = await context.ResetPasswords.FirstOrDefaultAsync(x => x.Id == id);

            if (resetPassword == null)
                return null;

            return new ResetPassword()
            {
                Id = resetPassword.Id,
                Guid = resetPassword.Guid,
                UserId = resetPassword.UserId
            };
        }

        public async Task<ResetPassword> GetByGuidAsync(Guid guid)
        {
            var resetPassword = await context.ResetPasswords.FirstOrDefaultAsync(x => x.Guid == guid);

            if (resetPassword == null)
                return null;

            return await GetByIdAsync(resetPassword.Id);
        }

        public async Task<ResetPassword> GetByUserIdAsync(int userId)
        {
            var resetPassword = await context.ResetPasswords.FirstOrDefaultAsync(x => x.UserId == userId);

            if (resetPassword == null)
                return null;

            return await GetByIdAsync(resetPassword.Id);
        }
    }
}
