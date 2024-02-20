using Core.Model.User;

namespace Core.Repository
{
    public interface IResetPasswordRepository
    {
        public Task<ResetPassword> GetByIdAsync(int id);
        public Task<ResetPassword> GetByUserIdAsync(int userId);
        public Task<ResetPassword> GetByGuidAsync(Guid guid);
        public Task<Guid> AddAsync(ResetPassword model);
        public Task DeleteAsync(Guid guid);
    }
}
