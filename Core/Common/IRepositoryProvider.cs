using Core.Repository;

namespace Core.Common
{
    public interface IRepositoryProvider
    {
        public IUserRepository UserRepository { get; }
        public IAccessPositionRepository AccessPositionRepository { get; }
        public ISettingsRepositroy SettingsRepositroy { get; }
        public ILogRepository LogRepository { get; }
        public IFileRepository FileRepository { get; }
        public ILocationRepository LocationRepository { get; }
        public IActivationRepository ActivationRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ITicketCategoryRepository TicketCategoryRepository { get; }
        public IResetPasswordRepository ResetPasswordRepository { get; }
    }
}
