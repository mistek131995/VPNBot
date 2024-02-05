using Core.Model.Ticket;

namespace Core.Repository
{
    public interface ITicketRepository
    {
        public Task<Ticket> GetByIdAsync(int id);
        public Task<List<Ticket>> GetByIdsAsync(List<int> ids);
        public Task<List<Ticket>> GetByUserIdAsync(int userId);
        public Task<Ticket> GetByTicketIdAndUserIdAsync(int ticketId, int userId);
        public Task<int> AddAsync(Ticket ticket);
        public Task UpdateAsync(Ticket ticket);
    }
}
