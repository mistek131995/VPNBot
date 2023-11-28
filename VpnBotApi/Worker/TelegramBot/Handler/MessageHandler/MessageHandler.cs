using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;
using AccountManagment = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.AccountManagment;
using DownloadApp = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.DownloadApp;
using Help = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.Help;


namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler
{
    internal class MessageHandler(HandlerDispatcher dispatcher)
    {
        private readonly HandlerDispatcher dispatcher = dispatcher;

        public async Task HandlingAsync(ITelegramBotClient client, Update update)
        {
            var message = update.Message;
            var chat = message.Chat;

            if (message.Text == "/start")
            {
                var replyMessage = await dispatcher.BuildHandler<MainMenu.Response, MainMenu.Query>(new MainMenu.Query(message.From.Id, chat.Id));

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Получить доступ")
            {
                var replyMessage = await dispatcher.BuildHandler<GetAccess.Response, GetAccess.Query>(new GetAccess.Query(message.From.Id));

                //Тут отправляется QR код
                if (replyMessage.AccessQrCode != null && replyMessage.AccessQrCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.AccessQrCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Аккаунт")
            {
                var replyMessage = await dispatcher.BuildHandler<AccountManagment.Response, AccountManagment.Query>(new AccountManagment.Query(message.From.Id));

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if (message.Text == "Скачать приложение")
            {
                var replyMessage = await dispatcher.BuildHandler<DownloadApp.Response, DownloadApp.Query>(new DownloadApp.Query());

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if (message.Text == "Помощь")
            {
                var replyMessage = await dispatcher.BuildHandler<Help.Response, Help.Query>(new Help.Query());

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else
            {
                //Отсюда берем только основное меню
                var replyMessage = await dispatcher.BuildHandler<GetAccess.Response, GetAccess.Query>(new GetAccess.Query(message.From.Id));

                await client.SendTextMessageAsync(chat.Id, "Команда не распознана, выберите действие из меню.", replyMarkup: replyMessage.ReplyKeyboard);
            }
        }
    }
}
