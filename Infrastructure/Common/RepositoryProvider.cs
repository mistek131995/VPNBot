using Core.Common;
using Core.Repository;
using Infrastructure.Database;
using Infrastructure.Database.Repository;

namespace Infrastructure.Common
{
    public class RepositoryProvider(Context context) : IRepositoryProvider
    {
        private IUserRepository userRepository;
        private IVpnServerRepository vpnServerRepository;
        private IAccessPositionRepository accessPositionsRepository;
        private ISettingsRepositroy settingsRepositroy;
        private ILogRepository logRepository;
        private IFileRepository fileRepository;

        public IUserRepository UserRepository =>
            userRepository ??= new UserRepository(context);

        public IVpnServerRepository VpnServerRepository => 
            vpnServerRepository ??= new VpnServerRepository(context);

        public IAccessPositionRepository AccessPositionRepository => 
            accessPositionsRepository ??= new AccessPositionRepository(context);

        public ISettingsRepositroy SettingsRepositroy => 
            settingsRepositroy ??= new SettingsRepository(context);

        public ILogRepository LogRepository => 
            logRepository ??= new LogRepository(context);

        public IFileRepository FileRepository => 
            fileRepository ??= new FileRepository(context);

    }
}
