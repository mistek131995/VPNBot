using Telegram.Bot;

namespace Infrastructure.TelegramService
{
    public interface ITelegramNotificationService
    {
        ITelegramNotificationService AddText(string text);
        ITelegramNotificationService AddButtonWithLink(string text, string link);
        Task SendNotificationAsync(long telegramChatId);
    }
}
