using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler;
using VpnBotApi.Worker.TelegramBot.Handler.MessageHandler;

namespace VPNBot.Handler.UpdateHandler
{
    internal class UpdateHandler(MessageHandler messageHandler, CallbackQueryHandler callbackQueryHandler)
    {
        private readonly MessageHandler messageHandler = messageHandler;
        private readonly CallbackQueryHandler callbackQueryHandler = callbackQueryHandler;

        public async Task HandlingAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    await messageHandler.HandlingAsync(client, update);
                }
                else if (update.Type == UpdateType.CallbackQuery)
                {
                    await callbackQueryHandler.HandlingAsync(client, update);
                }
            }
            catch (Exception ex)
            {
                if(ex.GetType() == typeof(UserOrAccessNotFountException)) 
                {
                    var chatId = 0l;
                    if (update.Type == UpdateType.Message)
                        chatId = update.Message.From.Id;
                    else if (update.Type == UpdateType.CallbackQuery)
                        chatId = update.CallbackQuery.Message.Chat.Id;

                    await client.SendTextMessageAsync(chatId, ex.Message);
                }
            }
        }
    }
}
