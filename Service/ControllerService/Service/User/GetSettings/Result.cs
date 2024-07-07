namespace Service.ControllerService.Service.User.GetSettings
{
    public class Result
    {
        public string Email { get; set; }
        public long TelegramId { get; set; }

        public bool UseTelegramNotificationTicketMessage { get; set; }
        public bool UseTelegramNotificationAboutNews { get; set; }
        public bool UseTelegramNotificationLoginInError { get; set; }
        public bool UseEmailNotificationTicketMessage { get; set; }
        public bool UseEmailNotificationAboutNews { get; set; }
        public bool UseEmailNotificationLoginInError { get; set; }
    }
}
