using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Entity
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string TelegramToken { get; set; }

        public string SSHServerIP { get; set; }
        public string SSHServerLogin { get; set; }
        public string SSHServerPassword { get; set; }
        public string FileBasePath { get; set; }

        public string CaptchaPublicKey { get; set; }
        public string CaptchaPrivateKey { get; set; }
    }
}
