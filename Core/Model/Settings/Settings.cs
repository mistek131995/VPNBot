using Core.Common;

namespace Core.Model.Settings
{
    public class Settings : IAggregate
    {
        public int Id { get; set; }
        public string TelegramToken { get; set; }
    }
}
