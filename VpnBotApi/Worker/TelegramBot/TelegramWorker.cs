using Core.Common;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace VpnBotApi.Worker.TelegramBot
{
    internal class TelegramWorker(UpdateHandler.UpdateHandler updateHandler, ErrorHandler.ErrorHandler errorHandler, IRepositoryProvider repositoryProvider)
    {
        private static ITelegramBotClient botClient;
        private static ReceiverOptions receiverOptions;

        public async Task StartBotAsync(CancellationToken cancellationToken)
        {
            var token = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

            if(string.IsNullOrEmpty(token.TelegramToken))
                return;

            botClient = new TelegramBotClient(token.TelegramToken);

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
