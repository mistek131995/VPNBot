using Core.Common;

namespace Core.Model.Settings
{
    public class Settings : IAggregate
    {
        public int Id { get; set; }
        public string TelegramToken { get; set; }
        public string SSHServerIP { get; set; }
        public string SSHServerLogin { get; set;}
        public string SSHServerPassword { get; set;}
        public string FileBasePath { get; set; }
    }
}
