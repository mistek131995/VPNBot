using Core.Model.Finance;

namespace Core.Repository
{
    public interface IAccessPositionRepository
    {
        public Task<AccessPosition> GetByIdAsync(int id);
        public Task<AccessPosition> GetByPriceAsync(int price);
        public Task<List<AccessPosition>> GetAllAsync();
    }
}
