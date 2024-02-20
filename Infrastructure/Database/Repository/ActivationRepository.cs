using Core.Model.User;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Database.Repository
{
    internal class ActivationRepository(Context context) : IActivationRepository
    {
        public async Task AddAsync(Activation activation)
        {
            await context.AddAsync(new Entity.Activation()
            {
                Guid = activation.Guid,
                UserId = activation.UserId,
            });

            await context.SaveChangesAsync();
        }

        public async Task DeleteByGuid(Guid guid)
        {
            var activation = await context.Activations.FirstOrDefaultAsync(x => x.Guid == guid);

            if(activation != null)
            {
                context.Remove(activation);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Activation> GetByGuidAsync(Guid guid)
        {
            var dbActivation = await context.Activations.FirstOrDefaultAsync(x => x.Guid == guid);

            if(dbActivation != null)
                return new Activation(dbActivation.Id, dbActivation.UserId, dbActivation.Guid);

            return null;
        }

        public async Task<Activation> GetByUserIdAsync(int userId)
        {
            var dbActivation = await context.Activations.FirstOrDefaultAsync(x => x.UserId == userId);

            if (dbActivation != null)
                return new Activation(dbActivation.Id, dbActivation.UserId, dbActivation.Guid);

            return null;
        }
    }
}
