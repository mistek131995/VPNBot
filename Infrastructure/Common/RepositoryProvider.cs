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

        public IUserRepository UserRepository =>
            userRepository ??= new UserRepository(context);

        public IVpnServerRepository VpnServerRepository => 
            vpnServerRepository ??= new VpnServerRepository(context);

        public IAccessPositionRepository AccessPositionRepository => 
            accessPositionsRepository ??= new AccessPositionRepository(context);
    }
}
