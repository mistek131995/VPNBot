using Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using VpnBotApi.Worker.TelegramBot.DatabaseHelper;
using VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.DownloadApp;
using VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess;
using VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.MainMenu;

namespace VPNBot.Handler.UpdateHandler.Message
{
    internal class MessageHandler
    {
        public static async Task HandlingAsync(ITelegramBotClient client, Update update, Context context)
        {
            var message = update.Message;
            var chat = message.Chat;


            if (message.Text == "/start")
            {
                await UserHelper.CreateUser(message.From.Id, context);

                var replyMessage = MainMenuMessage.Build();

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Скачать приложение")
            {
                var replyMessage = DownloadAppMessage.Build();

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if(message.Text == "Получить доступ")
            {
                var accesses = (await AccessHelper.GetAccessesByTelegramUserId(message.From.Id, context))
                    .Select(x => (x.Id, x.EndDate))
                    .ToList();

                var replyMessage = GetAccessMessage.Build(new List<(int id, DateTime endDate)>());

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
        }
    }
}
