using Application.TelegramBotService.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using BuyAccess = Service.TelegramBotService.Service.BuyAccess;
using FreeExtend = Service.TelegramBotService.Service.FreeExtend;
using GetQRCode = Service.TelegramBotService.Service.GetQRCode;

namespace VpnBotApi.Worker.TelegramBot.CallbackQueryHandler
{
    public class CallbackQueryHandler(TelegramBotServiceDispatcher dispatcher)
    {
        public async Task HandlingAsync(ITelegramBotClient client, Update update)
        {
            var callbackQuery = update.CallbackQuery;
            var user = callbackQuery.From;

            // Вот тут нужно уже быть немножко внимательным и не путаться!
            // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
            // кнопка привязана к сообщению, то мы берем информацию от сообщения.
            var chat = callbackQuery.Message.Chat;

            await client.AnswerCallbackQueryAsync(callbackQuery.Id);

            if (callbackQuery.Data == "accessPositionList")
            {
                var replyMessage = await dispatcher.GetService<BuyAccess.Result, BuyAccess.Request>(new BuyAccess.Request(user.Id));

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
            else if (callbackQuery.Data == "getQrCode")
            {
                var replyMessage = await dispatcher.GetService<GetQRCode.Result, GetQRCode.Request>(new GetQRCode.Request(user.Id));

                //Тут отправляется QR код
                if (replyMessage.QRCode != null && replyMessage.QRCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.QRCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text);
            }
            else if (callbackQuery.Data == "helpAndroid")
            {
                var text = "1. Скачайте и откройте приложение." +
                            "\n2. Сохраните QR, полученный у бота в галерею." +
                            "\n3. В верхнем, правом углу нажмите на '+' и выберите 'Импорт профиля из QR кода'." +
                            "\n4. В верхнем, правом углу значок галереи и выберите сохранённый QR код." +
                            "\n5. На главной странице приложения нажмите на круглую кнопку в правом, нижнем углу.";

                await client.SendTextMessageAsync(chat.Id, text);
            }
            else if (callbackQuery.Data == "helpIOS")
            {
                var text = "1. Скачайте и откройте приложение. " +
                            "\n2. Сохраните QR, полученный у бота в галерею. " +
                            "\n3. В верхнем, правом углу нажмите на '+' и выберите 'Отсканировать QRCode'. " +
                            "\n4. Выберите QR код и галереи. " +
                            "\n5. Включите VPN на главной странице приложения.";

                await client.SendTextMessageAsync(chat.Id, text);
            }
            else if (callbackQuery.Data == "freeExtend")
            {
                var replyMessage = await dispatcher.GetService<FreeExtend.Result, FreeExtend.Request>(new FreeExtend.Request(user.Id));

                //Тут отправляется QR код
                if (replyMessage.QRCode != null && replyMessage.QRCode.Length > 0)
                {
                    using (Stream stream = new MemoryStream(replyMessage.QRCode))
                    {

                        await client.SendPhotoAsync(chat.Id, InputFile.FromStream(stream));
                    }
                }

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text);
            }

            await client.DeleteMessageAsync(chat.Id, callbackQuery.Message.MessageId);
        }
    }
}
