using Database.Repository.Implementation;
using Database.Repository.Interface;

namespace Database.Common
{
    public class RepositoryProvider(Context context) : IRepositoryProvider
    {
        private Context context = context;

        private IUserRepository userRepository;
        private IAccessRepository accessRepository;

        public IUserRepository UserRepository =>
            userRepository ??= new UserRepository(context);

        public IAccessRepository AccessRepository =>
            accessRepository ??= new AccessRepository(context);
    }
}
