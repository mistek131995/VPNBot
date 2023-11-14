using Telegram.Bot;
using Telegram.Bot.Types;
using VPNBot.Handler.UpdateHandler.Message.MessageBuilder.DownloadApp;
using VPNBot.Handler.UpdateHandler.Message.MessageBuilder.MainMenu;

namespace VPNBot.Handler.UpdateHandler.Message
{
    internal class MessageHandler
    {
        public static async Task Handling(ITelegramBotClient client, Update update)
        {
            var message = update.Message;
            var chat = message.Chat;

            if (message.Text == "/start" || message.Text == "Вернуться назад")
            {
                var replyMessage = MainMenuMessage.Build();

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Скачать приложение")
            {
                var replyMessage = DownloadAppMessage.Build();

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
        }
    }
}
