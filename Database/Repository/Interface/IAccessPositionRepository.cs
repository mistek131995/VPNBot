using Database.Model;

namespace Database.Repository.Interface
{
    public interface IAccessPositionRepository
    {
        public Task<AccessPosition> GetByIdAsync(int id);
        public Task<List<AccessPosition>> GetAllAsync();
    }
}
