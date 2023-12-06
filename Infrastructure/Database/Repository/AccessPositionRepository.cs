using Core.Model.AccessPosition;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class AccessPositionRepository(Context context) : IAccessPositionRepository
    {
        public async Task<List<AccessPosition>> GetAllAsync()
        {
            return await context.AccessPositions.AsNoTracking().Select(x => new AccessPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MonthCount = x.MonthCount,
                Price = x.Price,
            }).ToListAsync();
        }

        public async Task<AccessPosition> GetByIdAsync(int id)
        {
            return await context.AccessPositions.AsNoTracking().Select(x => new AccessPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MonthCount = x.MonthCount,
                Price = x.Price,
            }).FirstOrDefaultAsync(x => x.Id == id);
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
            })
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Price == price);
        }
    }
}
