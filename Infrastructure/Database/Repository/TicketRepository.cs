using Core.Model.Ticket;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Database.Repository
{
    internal class TicketRepository(Context context) : ITicketRepository
    {
        public async Task<int> AddAsync(Ticket ticket)
        {
            var newTicket = new Entity.Ticket()
            {
                Title = ticket.Title,
                CreateDate = ticket.CreateDate,
                Condition = ticket.Condition,
                TicketCategoryId = ticket.CategoryId,
                UserId = ticket.UserId,
                TicketMessages = ticket.TicketMessages.Select(x =>  new Entity.TicketMessage() 
                { 
                    Message = x.Message,
                    Condition = x.Condition,
                    SendDate = x.SendDate,
                    UserId = x.UserId,
                }).ToList(),
            };
            await context.Tickets.AddAsync(newTicket);
            await context.SaveChangesAsync();


            return newTicket.Id;
        }

        public async Task<List<Ticket>> GetAllActiveAsync()
        {
            var ticketIds = await context.Tickets
                .AsNoTracking()
                .Where(x => x.Condition == TicketCondition.Open)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(ticketIds);
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            var ticket = await context.Tickets
                .Include(x => x.TicketCategory)
                .Include(x => x.TicketMessages)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ticket == null)
                return null;

            return new Ticket()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                CategoryName = ticket.TicketCategory.Name,
                Condition = ticket.Condition,
                CreateDate = ticket.CreateDate,
                UserId = ticket.UserId,
                TicketMessages = ticket.TicketMessages.Select(x => new TicketMessage()
                {
                    Id = x.Id,
                    Condition = x.Condition,
                    Message = x.Message,
                    SendDate = x.SendDate,
                    UserId = x.UserId,
                }).ToList(),
            };
        }

        public async Task<List<Ticket>> GetByIdsAsync(List<int> ids)
        {
            var tickets = await context.Tickets
                .Include(x => x.TicketCategory)
                .Include(x => x.TicketMessages)
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            return tickets.Select(x => new Ticket() { 
                Id = x.Id,
                Title = x.Title,
                CategoryName = x.TicketCategory.Name,
                Condition = x.Condition,
                CreateDate = x.CreateDate,
                UserId = x.UserId,
                TicketMessages = x.TicketMessages.Select(m => new TicketMessage()
                {
                    Id = m.Id,
                    Message = m.Message,
                    Condition = m.Condition,
                    SendDate = m.SendDate,
                    UserId = m.UserId
                }).ToList()
            }).ToList();
        }

        public async Task<Ticket> GetByTicketIdAndUserIdAsync(int ticketId, int userId)
        {
            var ticket = await context.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId && x.UserId == userId);

            if (ticket == null)
                return null;

            return await GetByIdAsync(ticket.Id);
        }

        public async Task<List<Ticket>> GetByUserIdAsync(int userId)
        {
            var ticketIds = await context.Tickets
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .ToListAsync();

            return await GetByIdsAsync(ticketIds);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            var dbTicket = await context.Tickets
                .Include(x => x.TicketMessages)
                .FirstOrDefaultAsync(x => x.Id == ticket.Id);

            var newMessages = ticket.TicketMessages
                .Where(x => x.Id == 0)
                .Select(m => new Entity.TicketMessage()
                {
                    Id = m.Id,
                    Message = m.Message,
                    UserId = m.UserId,
                    Condition = m.Condition,
                    SendDate = m.SendDate,
                    TicketId = ticket.Id,
                })
                .ToList();

            dbTicket.TicketMessages.AddRange(newMessages);
            context.Tickets.Update(dbTicket);

            await context.SaveChangesAsync();
        }
    }
}
