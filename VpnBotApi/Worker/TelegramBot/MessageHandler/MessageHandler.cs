using Application.TelegramBotService.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Start = Service.TelegramBotService.Service.Start;
using GetAccess = Service.TelegramBotService.Service.GetAccess;
using AccountManagment = Service.TelegramBotService.Service.AccountManagment;
using UnknowCommand = Service.TelegramBotService.Service.UnknowCommand;
using Service.TelegramBotService.Common;


namespace VpnBotApi.Worker.TelegramBot.MessageHandler
{
    internal class MessageHandler(TelegramBotServiceDispatcher dispatcher)
    {
        public async Task HandlingAsync(ITelegramBotClient client, Update update)
        {
            var message = update.Message;
            var chat = message.Chat;

            if (message.Text == "/start")
            {
                var replyMessage = await dispatcher.GetService<Start.Result, Start.Request>(new Start.Request(message.From.Id, chat.Id));

                if (replyMessage.QRCode != null && replyMessage.QRCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.QRCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Получить доступ")
            {
                var replyMessage = await dispatcher.GetService<GetAccess.Result, GetAccess.Request>(new GetAccess.Request(message.From.Id));

                if (replyMessage.QRCode != null && replyMessage.QRCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.QRCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
            else if (message.Text == "Аккаунт")
            {
                var replyMessage = await dispatcher.GetService<AccountManagment.Result, AccountManagment.Request>(new AccountManagment.Request(message.From.Id));

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if (message.Text == "Скачать приложение")
            {
                var inlineKeyboard = ButtonTemplate.GetDownloadButton();
                var text = "Выберите приложение для скачивания:";

                await client.SendTextMessageAsync(chat.Id, text, replyMarkup: inlineKeyboard);
            }
            else if (message.Text == "Помощь")
            {
                var inlineKeyboard = ButtonTemplate.GetHelpButton();
                var text = "Выберите раздел:";

                await client.SendTextMessageAsync(chat.Id, text, replyMarkup: inlineKeyboard);
            }
            else
            {
                //Отсюда берем только основное меню
                var replyMessage = await dispatcher.GetService<UnknowCommand.Result, UnknowCommand.Request>(new UnknowCommand.Request(message.From.Id, chat.Id));

                await client.SendTextMessageAsync(chat.Id, "Команда не распознана, выберите действие из меню.", replyMarkup: replyMessage.ReplyKeyboard);
            }
        }
    }
}
