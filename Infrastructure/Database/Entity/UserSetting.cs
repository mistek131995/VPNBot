using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool UseTelegramNotificationTicketMessage { get; set; }


        public User User { get; set; }
    }
}
