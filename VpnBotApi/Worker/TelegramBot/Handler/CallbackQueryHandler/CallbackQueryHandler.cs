﻿using Telegram.Bot;
using Telegram.Bot.Types;
using VpnBotApi.Worker.TelegramBot.Common;
using GetAccess = VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.GetAccess;
using ExtendForMonth = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.ExtendForMonth;
using static System.Net.Mime.MediaTypeNames;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler
{
    public class CallbackQueryHandler(HandlerDispatcher dispatcher)
    {
        private readonly HandlerDispatcher dispatcher = dispatcher;

        public async Task HandlingAsync(ITelegramBotClient client, Update update)
        {
            var callbackQuery = update.CallbackQuery;
            var user = callbackQuery.From;

            // Вот тут нужно уже быть немножко внимательным и не путаться!
            // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
            // кнопка привязана к сообщению, то мы берем информацию от сообщения.
            var chat = callbackQuery.Message.Chat;

            if (callbackQuery.Data == "extendForMonth")
            {
                var replyMessage = await dispatcher.BuildHandler<ExtendForMonth.Response, ExtendForMonth.Query>(new ExtendForMonth.Query(user.Id));

                //Тут отправляется QR код
                if (replyMessage.AccessQrCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.AccessQrCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text);

            }
            else if(callbackQuery.Data == "getQrCode")
            {
                var replyMessage = await dispatcher.BuildHandler<GetAccess.Response, GetAccess.Query>(new GetAccess.Query(user.Id));

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
            else if(callbackQuery.Data == "helpAndroid")
            {
                var text = @"
1. Скачайте и откройте приложение.
2. Сохраните QR, полученный у бота в галерею.
3. В верхнем, правом углу нажмите на '+' и выберите 'Импорт профиля из QR кода'.
4. В верхнем, правом углу значок галереи и выберите сохранённый QR код.
5. На главной странице приложения нажмите на круглую кнопку в правом, нижнем углу.";

                await client.SendTextMessageAsync(chat.Id, text);
            }
            else if(callbackQuery.Data == "helpIOS")
            {
                var text = @"Будет позже, поскольку у меня нет яблока. Насколько я помню, там ничего сложного =).";

                await client.SendTextMessageAsync(chat.Id, text);
            }

            await client.AnswerCallbackQueryAsync(callbackQuery.Id);
        }
    }
}
