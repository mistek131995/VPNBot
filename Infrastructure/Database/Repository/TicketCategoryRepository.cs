using Core.Model.Support;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class TicketCategoryRepository(Context context) : ITicketCategoryRepository
    {
        public async Task<List<TicketCategory>> GetAllAsync()
        {
            return await context.TicketCategories
                .AsNoTracking()
                .Select(c => new TicketCategory()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
