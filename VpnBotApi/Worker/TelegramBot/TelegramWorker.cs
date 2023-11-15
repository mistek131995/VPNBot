using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using VPNBot.Handler.ErrorHandler;
using VPNBot.Handler.UpdateHandler;
using Telegram.Bot.Types;
using Database;

namespace VpnBotApi.Worker.TelegramBot
{
    public class TelegramWorker
    {
        private static ITelegramBotClient botClient;
        private static ReceiverOptions receiverOptions;
        private readonly Context context;

        public TelegramWorker(Context context) => 
            this.context = context;

        public async Task StartBotAsync(CancellationToken cancellationToken)
        {
            botClient = new TelegramBotClient("6619723942:AAHI2FPYo2P0ESn_9D1SHiaA1m0Mb3QwKq0");

            receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                    UpdateType.CallbackQuery
                },

                // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
                // True - не обрабатывать, False (стоит по умолчанию) - обрабатывать
                ThrowPendingUpdates = true
            };

            using var cts = new CancellationTokenSource();

            botClient.StartReceiving(
                (ITelegramBotClient client, Update update, CancellationToken token) => UpdateHandler.HandlingAsync(client, update, token, context), 
                ErrorHandler.HandlingAsync, 
                receiverOptions, 
                cts.Token); // Запускаем бота

            await Task.Delay(-1);
        }
    }
}
