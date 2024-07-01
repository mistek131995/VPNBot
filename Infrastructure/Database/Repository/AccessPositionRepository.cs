using Core.Model.Finance;
using Core.Repository;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class AccessPositionRepository(ContextFactory context) : IAccessPositionRepository
    {
        public async Task AddAsync(AccessPosition accessPosition)
        {
            context.AccessPositions.Add(new Entity.AccessPosition()
            {
                Name = accessPosition.Name,
                Description = accessPosition.Description,
                GooglePlayIdentifier = accessPosition.GooglePlayIdentifier,
                MonthCount = accessPosition.MonthCount,
                Price = accessPosition.Price,
            });

            await context.SaveChangesAsync();
        }

        public async Task<List<AccessPosition>> GetAllAsync()
        {
            var ids = context.AccessPositions.Select(x => x.Id).ToList();

            return await GetByIds(ids);
        }

        public async Task<AccessPosition> GetByIdAsync(int id)
        {
            var accessPosition = await context.AccessPositions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(accessPosition == null)
                return null;

            return new AccessPosition(accessPosition.Id, accessPosition.Name, accessPosition.MonthCount, accessPosition.Description, accessPosition.Price, accessPosition.GooglePlayIdentifier);
        }

        public async Task<List<AccessPosition>> GetByIds(List<int> ids)
        {
            return await context.AccessPositions
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => new AccessPosition(x.Id, x.Name, x.MonthCount, x.Description, x.Price, x.GooglePlayIdentifier))
                .ToListAsync();
        }

        public async Task<AccessPosition> GetByPriceAsync(int price)
        {
            var accessPosition = await context.AccessPositions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Price == price);

            if(accessPosition == null) return null;

            return await GetByIdAsync(accessPosition.Id);
        }

        public async Task UpdateAsync(AccessPosition accessPosition)
        {
            var position = await context.AccessPositions.FirstOrDefaultAsync(x => x.Id == accessPosition.Id);
            position.Name = accessPosition.Name;
            position.Description = accessPosition.Description;
            position.MonthCount = accessPosition.MonthCount;
            position.Price = accessPosition.Price;
            position.GooglePlayIdentifier = accessPosition.GooglePlayIdentifier;

            await context.SaveChangesAsync();
        }
    }
}
