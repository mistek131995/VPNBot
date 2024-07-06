using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool UseTelegramNotificationTicketMessage { get; set; }
        public bool UseTelegramNotificationAboutNews { get; private set; }
        public bool UseTelegramNotificationLoginInError { get; private set; }
        public bool EmailNotificationTicketMessage { get; private set; }
        public bool EmailNotificationAboutNews { get; private set; }
        public bool EmailNotificationLoginInError { get; private set; }


        public User User { get; set; }
    }
}
