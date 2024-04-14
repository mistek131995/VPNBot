using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string TelegramToken { get; set; }

        public string CaptchaPublicKey { get; set; }
        public string CaptchaPrivateKey { get; set; }

        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPLogin { get; set; }
        public string SMTPPassword { get; set; }
    }
}
