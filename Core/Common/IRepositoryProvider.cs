using Core.Repository;

namespace Core.Common
{
    public interface IRepositoryProvider
    {
        public IUserRepository UserRepository { get; }
        public IVpnServerRepository VpnServerRepository { get; }
        public IAccessPositionRepository AccessPositionRepository { get; }
        public ISettingsRepositroy SettingsRepositroy { get; }
        public ILogRepository LogRepository { get; }
        public IFileRepository FileRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IActiovationRepository ActiovationRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ITicketCategoryRepository TicketCategoryRepository { get; }
    }
}
