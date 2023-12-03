using Core.Repository;

namespace Core.Common
{
    public interface IRepositoryProvider
    {
        public IUserRepository UserRepository { get; }
        public IVpnServerRepository VpnServerRepository { get; }
        public IAccessPositionRepository AccessPositionRepository { get; }
    }
}
