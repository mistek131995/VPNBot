using Core.Common;
using Core.Repository;
using Infrastructure.Database;
using Infrastructure.Database.Repository;

namespace Infrastructure.Common
{
    public class RepositoryProvider(Context context) : IRepositoryProvider
    {
        private IUserRepository userRepository;
        private IAccessPositionRepository accessPositionsRepository;
        private ISettingsRepositroy settingsRepositroy;
        private ILogRepository logRepository;
        private IFileRepository fileRepository;
        private ILocationRepository locationRepository;
        private IActivationRepository activationRepository;
        private ITicketRepository ticketRepository;
        private ITicketCategoryRepository ticketCategoryRepository;
        private IResetPasswordRepository resetPasswordRepository;
        private IPromoCodeRepository promoCodeRepository;

        public IUserRepository UserRepository =>
            userRepository ??= new UserRepository(context);

        public IAccessPositionRepository AccessPositionRepository => 
            accessPositionsRepository ??= new AccessPositionRepository(context);

        public ISettingsRepositroy SettingsRepositroy => 
            settingsRepositroy ??= new SettingsRepository(context);

        public ILogRepository LogRepository => 
            logRepository ??= new LogRepository(context);

        public IFileRepository FileRepository => 
            fileRepository ??= new FileRepository(context);

        public ILocationRepository LocationRepository => 
            locationRepository ??= new LocationRepository(context);

        public IActivationRepository ActivationRepository => 
            activationRepository ??= new ActivationRepository(context);

        public ITicketRepository TicketRepository => 
            ticketRepository ??= new TicketRepository(context);

        public ITicketCategoryRepository TicketCategoryRepository => 
            ticketCategoryRepository ??= new TicketCategoryRepository(context);

        public IResetPasswordRepository ResetPasswordRepository => 
            resetPasswordRepository ??= new ResetPasswordRepository(context);

        public IPromoCodeRepository PromoCodeRepository => 
            promoCodeRepository ??= new PromoCodeRepository(context);
    }
}
