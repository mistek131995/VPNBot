using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string TelegramToken { get; set; }
    }
}
