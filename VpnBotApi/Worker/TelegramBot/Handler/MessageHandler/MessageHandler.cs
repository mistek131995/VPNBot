using Telegram.Bot;
using Telegram.Bot.Types;
using VpnBotApi.Worker.TelegramBot.Common;
using SubscribeManagement = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.SubscribeManagement;
using DownloadApp = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.DownloadApp;


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
                var replyMessage = await dispatcher.BuildHandler<MainMenu.Response, MainMenu.Query>(new MainMenu.Query(message.From.Id));

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Скачать приложение")
            {
                var replyMessage = await dispatcher.BuildHandler<DownloadApp.Response, DownloadApp.Query>(new DownloadApp.Query());

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if (message.Text == "Получить доступ")
            {
                var replyMessage = await dispatcher.BuildHandler<GetAccess.Response, GetAccess.Query>(new GetAccess.Query(message.From.Id));

                //Тут отправляется QR код
                if (replyMessage.AccessQrCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.AccessQrCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Управление подпиской")
            {
                var replyMessage = await dispatcher.BuildHandler<SubscribeManagement.Response, SubscribeManagement.Query>(new SubscribeManagement.Query(message.From.Id));

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if (message.Text == "Сообщить об ошибке")
            {
                await client.SendTextMessageAsync(chat.Id, "Временно недоступно.");
            }
            else
            {
                await client.SendTextMessageAsync(chat.Id, "Команда не распознана, выберите действие из меню.");
            }
        }
    }
}
