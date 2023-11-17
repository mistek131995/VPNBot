using Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VPNBot.Handler.UpdateHandler.Message;
using VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.CallbackQuery;

namespace VPNBot.Handler.UpdateHandler
{
    internal class UpdateHandler
    {
        private readonly MessageHandler messageHandler;

        public UpdateHandler(MessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public async Task HandlingAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    await messageHandler.HandlingAsync(client, update);
                }
                else if(update.Type == UpdateType.CallbackQuery)
                {
                    await CallbackQueryHandler.HandlingAsync(client, update);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
