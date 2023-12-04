using Core.Repository;

namespace Core.Common
{
    public interface IRepositoryProvider : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IVpnServerRepository VpnServerRepository { get; }
        public IAccessPositionRepository AccessPositionRepository { get; }
        public ISettingsRepositroy SettingsRepositroy { get; }
    }
}
