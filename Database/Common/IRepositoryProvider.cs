using Database.Repository.Interface;

namespace Database.Common
{
    public interface IRepositoryProvider
    {
        public IUserRepository UserRepository { get; }
        public IAccessRepository AccessRepository { get; }
    }
}
