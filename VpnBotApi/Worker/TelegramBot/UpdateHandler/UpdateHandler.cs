using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace VpnBotApi.Worker.TelegramBot.UpdateHandler
{
    internal class UpdateHandler(MessageHandler.MessageHandler messageHandler, CallbackQueryHandler.CallbackQueryHandler callbackQueryHandler, Serilog.ILogger logger)
    {

        public async Task HandlingAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message)
            {
                await messageHandler.HandlingAsync(client, update);
            }
        }
    }
}
