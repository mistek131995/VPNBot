using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public long TelegramChatId { get; set; }
        public string Password { get; set; }

        public Access Access { get; set; }
    }
}
