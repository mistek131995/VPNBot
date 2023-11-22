﻿using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using VPNBot.Handler.ErrorHandler;
using VPNBot.Handler.UpdateHandler;

namespace VpnBotApi.Worker.TelegramBot
{
    internal class TelegramWorker
    {
        private static ITelegramBotClient botClient;
        private static ReceiverOptions receiverOptions;
        private readonly UpdateHandler updateHandler;
        private readonly ErrorHandler errorHandler;
        public TelegramWorker(UpdateHandler updateHandler, ErrorHandler errorHandler)
        {
            this.updateHandler = updateHandler;
            this.errorHandler = errorHandler;
        }

        public async Task StartBotAsync(CancellationToken cancellationToken)
        {
            botClient = new TelegramBotClient("6846861964:AAHM_mMcnAeeXhNoRYotK78-oy2-399w5y4");

            receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                }
            };

            using var cts = new CancellationTokenSource();

            botClient.StartReceiving(updateHandler.HandlingAsync, errorHandler.HandlingAsync, receiverOptions, cts.Token); // Запускаем бота

            await Task.Delay(-1);
        }
    }
}
