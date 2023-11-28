using Database.Model;

namespace Database.Repository.Interface
{
    public interface IAccessPositionRepository
    {
        public Task<List<AccessPosition>> GetAllAsync();
    }
}
