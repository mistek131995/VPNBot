using Core.Common;
using Telegram.Bot;

namespace Infrastructure.TelegramService
{
    internal class TelegramNotificationService(IRepositoryProvider repositoryProvider) : ITelegramNotificationService
    {
        public async Task SendNotificationAsync(string message, long telegramChatId)
        {
            var client = await GetClientAsync();

            await client.SendTextMessageAsync(telegramChatId, message);
        }

        private async Task<TelegramBotClient> GetClientAsync()
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            return new TelegramBotClient(settings.TelegramToken);
        }
    }
}
