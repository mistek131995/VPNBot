using Core.Model.AccessPosition;

namespace Core.Repository
{
    public interface IAccessPositionRepository
    {
        public Task<List<AccessPosition>> GetAllAsync();
    }
}
