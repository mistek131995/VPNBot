using Core.Model.Ticket;
using Core.Repository;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal class TicketCategoryRepository(ContextFactory context) : ITicketCategoryRepository
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
