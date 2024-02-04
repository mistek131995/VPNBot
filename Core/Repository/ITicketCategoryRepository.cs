using Core.Model.Support;

namespace Core.Repository
{
    public interface ITicketCategoryRepository
    {
        public Task<List<TicketCategory>> GetAllAsync();
    }
}
