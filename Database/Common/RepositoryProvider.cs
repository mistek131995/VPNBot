using Database.Repository.Implementation;
using Database.Repository.Interface;

namespace Database.Common
{
    public class RepositoryProvider(Context context) : IRepositoryProvider
    {
        private IUserRepository userRepository;
        private IAccessRepository accessRepository;
        private ISettingsRepository settingsRepository;
        private IVpnServerRepository vpnServerRepository;
        private IAccessPositionRepository accessPositionRepository;

        public IUserRepository UserRepository =>
            userRepository ??= new UserRepository(context);

        public IAccessRepository AccessRepository =>
            accessRepository ??= new AccessRepository(context);

        public ISettingsRepository SettingsRepository => 
            settingsRepository ??= new SettingsRepository(context);

        public IVpnServerRepository VpnServerRepository => 
            vpnServerRepository ??= new VpnServerRepository(context);

        public IAccessPositionRepository AccessPositionRepository => 
            accessPositionRepository ??= new AccessPositionRepository(context);
    }
}
