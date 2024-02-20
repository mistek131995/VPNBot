using Core.Model.User;

namespace Core.Repository
{
    public interface IActivationRepository
    {
        public Task<Activation> GetByGuidAsync(Guid guid);
        public Task<Activation> GetByUserIdAsync(int userId);
        public Task AddAsync(Activation activation);
        public Task DeleteByGuid(Guid guid);
    }
}
