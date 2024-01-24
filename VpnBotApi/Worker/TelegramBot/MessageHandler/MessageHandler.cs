﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;


namespace VpnBotApi.Worker.TelegramBot.MessageHandler
{
    internal class MessageHandler()
    {
        public async Task HandlingAsync(ITelegramBotClient client, Update update)
        {
            var message = update.Message;
            var chat = message.Chat;

            var replyKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Сайт", "https://lockvpn.me/"),
                    InlineKeyboardButton.WithUrl("Вход", "https://lockvpn.me/login"),
                    InlineKeyboardButton.WithUrl("Регистрация", "https://lockvpn.me/register"),
                }
            });

            await client.SendTextMessageAsync(chat.Id, @"Телеграм бот ушел на залуженный отдых, его функционал будет пересмотрен. \n
На данный момент VPN доступен только по платной подписке и только для устройств на Windows и Android (Все подписки продолжат действовать до конца срока). 
Купить подписку можно в личном кабинете. Что касается IOS, ситуация доконца не ясна. 
Возможно, я прикручу старый способ с QR кодами к личному кабинету или оставлю это до лучших времен. 
(Приложения под IOS можно писать только на MacOS, а выкидывать 60к для этого я не готов, пока что).", replyMarkup: replyKeyboard);

        }
    }
}
