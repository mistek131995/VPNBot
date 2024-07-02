using Core.Common;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.TelegramService
{
    internal class TelegramNotificationService(IRepositoryProvider repositoryProvider) : ITelegramNotificationService
    {
        private string Text { get; set; }
        private List<InlineKeyboardButton> Buttons = new List<InlineKeyboardButton>();

        public ITelegramNotificationService AddButtonWithLink(string text, string link)
        {
            Buttons.Add(InlineKeyboardButton.WithUrl(text, link));

            return this;
        }

        public ITelegramNotificationService AddText(string text)
        {
            if(string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            Text = text;

            return this;
        }

        public async Task SendNotificationAsync(long telegramChatId)
        {
            var client = await GetClientAsync();

            await client.SendTextMessageAsync(telegramChatId, Text, replyMarkup: Buttons.Count > 0 ? new InlineKeyboardMarkup(Buttons) : null);
        }

        private async Task<TelegramBotClient> GetClientAsync()
        {
            var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            return new TelegramBotClient(settings.TelegramToken);
        }
    }
}
