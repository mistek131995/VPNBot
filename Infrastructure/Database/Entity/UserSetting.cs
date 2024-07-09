using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool UseTelegramNotificationTicketMessage { get; set; }
        public bool UseTelegramNotificationAboutNews { get; set; }
        public bool UseTelegramNotificationLoginInError { get; set; }
        public bool UseEmailNotificationTicketMessage { get; set; }
        public bool UseEmailNotificationAboutNews { get; set; }
        public bool UseEmailNotificationLoginInError { get; set; }


        public User User { get; set; }
    }
}
