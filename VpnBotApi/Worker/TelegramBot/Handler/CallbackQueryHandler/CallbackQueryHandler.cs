﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler
{
    public class CallbackQueryHandler
    {
        public async Task HandlingAsync(ITelegramBotClient client, Update update)
        {
            var callbackQuery = update.CallbackQuery;
            var user = callbackQuery.From;

            // Вот тут нужно уже быть немножко внимательным и не путаться!
            // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
            // кнопка привязана к сообщению, то мы берем информацию от сообщения.
            var chat = callbackQuery.Message.Chat;

            if (callbackQuery.Data == "payForMonth")
            {
                //var replyMessage = await NewAccessService.GetNewAccess(user.Id, context);

                //await client.AnswerCallbackQueryAsync(callbackQuery.Id);
                //await client.SendTextMessageAsync(chat.Id, replyMessage.Text);

                //if(replyMessage.AccessQrCode.Length > 0) 
                //{ 
                //    using(Stream stream = new  MemoryStream(replyMessage.AccessQrCode))
                //    {

                //        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream)); 
                //    }
                //}
            }
            else if(callbackQuery.Data == "getQrCode")
            {

            }
        }
    }
}
