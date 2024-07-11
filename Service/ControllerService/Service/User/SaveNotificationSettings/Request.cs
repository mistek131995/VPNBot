using Application.ControllerService.Common;

namespace Service.ControllerService.Service.User.SaveNotificationSettings
{
    public class Request : IRequest<bool>
    {
        public int UserId { get; set; }
        public bool UseTelegramNotificationTicketMessage { get; set; }
        public bool UseTelegramNotificationAboutNews { get; set; }
        public bool UseTelegramNotificationLoginInError { get; set; }
        public bool UseEmailNotificationTicketMessage { get; set; }
        public bool UseEmailNotificationAboutNews { get; set; }
        public bool UseEmailNotificationLoginInError { get; set; }
    }
}
