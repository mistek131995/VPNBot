namespace Core.Model.User
{
    public class UserSetting
    {
        public int Id { get; private set; }
        public bool UseTelegramNotificationTicketMessage { get; set; }
        public bool UseTelegramNotificationAboutNews {  get; set; }
        public bool UseTelegramNotificationLoginInError { get; set; }
        public bool UseEmailNotificationTicketMessage { get; set; }
        public bool UseEmailNotificationAboutNews { get; set; }
        public bool UseEmailNotificationLoginInError { get; set; }

        public UserSetting(int id, bool useTelegramNotificationTicketMessage, bool useTelegramNotificationAboutNews, bool useTelegramNotificationLoginInError, 
            bool useEmailNotificationTicketMessage, bool useEmailNotificationAboutNews, bool useEmailNotificationLoginInError)
        {
            Id = id;
            UseTelegramNotificationTicketMessage = useTelegramNotificationTicketMessage;
            UseTelegramNotificationAboutNews = useTelegramNotificationAboutNews;
            UseTelegramNotificationLoginInError = useTelegramNotificationLoginInError;
            UseEmailNotificationAboutNews = useEmailNotificationAboutNews;
            UseEmailNotificationLoginInError = useEmailNotificationLoginInError;
            UseEmailNotificationTicketMessage = useEmailNotificationTicketMessage;
        }

        public UserSetting(bool useTelegramNotificationTicketMessage, bool useTelegramNotificationAboutNews, bool useTelegramNotificationLoginInError,
            bool useEmailNotificationTicketMessage, bool useEmailNotificationAboutNews, bool useEmailNotificationLoginInError)
        {
            UseTelegramNotificationTicketMessage = useTelegramNotificationTicketMessage;
            UseTelegramNotificationAboutNews = useTelegramNotificationAboutNews;
            UseTelegramNotificationLoginInError = useTelegramNotificationLoginInError;
            UseEmailNotificationAboutNews = useEmailNotificationAboutNews;
            UseEmailNotificationLoginInError = useEmailNotificationLoginInError;
            UseEmailNotificationTicketMessage = useEmailNotificationTicketMessage;
        }
    }
}
