namespace Core.Model.User
{
    public class UserSetting
    {
        public int Id { get; private set; }
        public bool UseTelegramNotificationTicketMessage { get; private set; }
        public bool UseTelegramNotificationAboutNews {  get; private set; }
        public bool UseTelegramNotificationLoginInError { get; private set; }
        public bool EmailNotificationTicketMessage { get; private set; }
        public bool EmailNotificationAboutNews { get; private set; }
        public bool EmailNotificationLoginInError { get; private set; }

        public UserSetting(int id, bool useTelegramNotificationTicketMessage, bool useTelegramNotificationAboutNews, bool useTelegramNotificationLoginInError)
        {
            Id = id;
            UseTelegramNotificationTicketMessage = useTelegramNotificationTicketMessage;
            UseTelegramNotificationAboutNews = useTelegramNotificationAboutNews;
            UseTelegramNotificationLoginInError = useTelegramNotificationLoginInError;
        }

        public UserSetting(bool useTelegramNotificationTicketMessage, bool useTelegramNotificationAboutNews, bool useTelegramNotificationLoginInError)
        {
            UseTelegramNotificationTicketMessage = useTelegramNotificationTicketMessage;
            UseTelegramNotificationAboutNews = useTelegramNotificationAboutNews;
            UseTelegramNotificationLoginInError = useTelegramNotificationLoginInError;
        }
    }
}
