using Telegram.Bot;
using Telegram.Bot.Types;
using VpnBotApi.Worker.TelegramBot.Common;
using GetAccess = VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.GetAccess;
using ExtendForMonth = VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.ExtendForMonth;

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

                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
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

                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.ReplyKeyboard);
            }
        }
    }
}
