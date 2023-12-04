using Core.Model.AccessPosition;

namespace Core.Repository
{
    public interface IAccessPositionRepository
    {
        public Task<AccessPosition> GetByIdAsync(int id);
        public Task<List<AccessPosition>> GetAllAsync();
    }
}
