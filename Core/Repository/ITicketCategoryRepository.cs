using Core.Model.Ticket;

namespace Core.Repository
{
    public interface ITicketCategoryRepository
    {
        public Task<List<TicketCategory>> GetAllAsync();
    }
}
