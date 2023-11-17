﻿using Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using VpnBotApi.Worker.TelegramBot.Common;
using MainMenu = VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.MainMenu;
using DownloadApp = VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.DownloadApp;
using GetAccess = VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess;

namespace VPNBot.Handler.UpdateHandler.Message
{
    internal class MessageHandler
    {
        private readonly Context context;
        private readonly HandlerDispatcher dispatcher;

        public MessageHandler(Context context, HandlerDispatcher dispatcher)
        {
            this.context = context;
            this.dispatcher = dispatcher;
        }

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

                await client.SendTextMessageAsync(chat.Id, replyMessage.Text, replyMarkup: replyMessage.InlineKeyboard);
            }
        }
    }
}
