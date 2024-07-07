namespace Core.Model.User
{
    public class UserSetting
    {
        public int Id { get; private set; }
        public bool UseTelegramNotificationTicketMessage { get; set; }
        public bool UseTelegramNotificationAboutNews {  get; set; }
        public bool UseTelegramNotificationLoginInError { get; set; }
        public bool EmailNotificationTicketMessage { get; set; }
        public bool EmailNotificationAboutNews { get; set; }
        public bool EmailNotificationLoginInError { get; set; }

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
