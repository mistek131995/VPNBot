using Core.Model.Finance;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class AccessPositionRepository(Context context) : IAccessPositionRepository
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
            return await context.AccessPositions
                .AsNoTracking()
                .Select(x => new AccessPosition()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MonthCount = x.MonthCount,
                    Price = x.Price,
                    GooglePlayIdentifier = x.GooglePlayIdentifier,
                })
                .ToListAsync();
        }

        public async Task<AccessPosition> GetByIdAsync(int id)
        {
            var accessPosition = await context.AccessPositions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(accessPosition == null)
                return null;

            return new AccessPosition()
            {
                Id = accessPosition.Id,
                Name = accessPosition.Name,
                Description = accessPosition.Description,
                MonthCount = accessPosition.MonthCount,
                Price = accessPosition.Price,
                GooglePlayIdentifier = accessPosition.GooglePlayIdentifier
            };
        }

        public async Task<AccessPosition> GetByPriceAsync(int price)
        {
            return await context.AccessPositions.Select(x => new AccessPosition()
            {
                Id = x.Id,
                Price = x.Price,
                Description = x.Description,
                MonthCount = x.MonthCount,
                Name = x.Name,
                GooglePlayIdentifier= x.GooglePlayIdentifier
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Price == price);
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
