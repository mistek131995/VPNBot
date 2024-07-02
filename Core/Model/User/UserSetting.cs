namespace Core.Model.User
{
    public class UserSetting
    {
        public int Id { get; private set; }
        public bool UseTelegramNotificationTicketMessage { get; private set; }

        public UserSetting(int id, bool useTelegramNotificationTicketMessage)
        {
            Id = id;
            UseTelegramNotificationTicketMessage = useTelegramNotificationTicketMessage;
        }

        public UserSetting(bool useTelegramNotificationTicketMessage)
        {
            UseTelegramNotificationTicketMessage = useTelegramNotificationTicketMessage;
        }
    }
}
