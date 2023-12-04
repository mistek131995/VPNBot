using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string TelegramToken { get; set; }
    }
}
