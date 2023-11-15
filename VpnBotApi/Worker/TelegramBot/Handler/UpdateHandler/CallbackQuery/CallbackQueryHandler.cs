using Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.CallbackQuery.GetNewAccess;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.CallbackQuery
{
    public class CallbackQueryHandler
    {
        public static async Task HandlingAsync(ITelegramBotClient client, Update update, Context context)
        {
            var callbackQuery = update.CallbackQuery;
            var user = callbackQuery.From;

            // Вот тут нужно уже быть немножко внимательным и не путаться!
            // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
            // кнопка привязана к сообщению, то мы берем информацию от сообщения.
            var chat = callbackQuery.Message.Chat;

            if (callbackQuery.Data == "new-access") 
            {
                var replyMessage = await NewAccessService.GetNewAccess(user.Id, context);

                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
                await client.SendTextMessageAsync(chat.Id, replyMessage.Text);

                if(replyMessage.AccessQrCode.Length > 0) 
                { 
                    using(Stream stream = new  MemoryStream(replyMessage.AccessQrCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream)); 
                    }
                }
 
            }
        }
    }
}
