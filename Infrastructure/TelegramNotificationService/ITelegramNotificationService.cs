using Telegram.Bot;

namespace Infrastructure.TelegramService
{
    public interface ITelegramNotificationService
    {
        Task SendNotificationAsync(string message, long telegramChatId);
    }
}
