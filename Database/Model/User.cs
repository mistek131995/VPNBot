using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public long TelegramChatId { get; set; }
        public string Login {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public Access Access { get; set; }

        public enum UserRole
        {
            Blocked = 0,
            User = 1,
            Admin = 2
        }
    }
}
