using Core.Model.User;

namespace Core.Repository
{
    public interface IActiovationRepository
    {
        public Task<Activation> GetByGuidAsync(Guid guid);
        public Task<Activation> GetByUserIdAsync(int userId);
        public Task AddActivationAsync(Activation activation);
        public Task DeleteByGuid(Guid guid);
    }
}
