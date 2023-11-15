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
        public static async Task HandlingAsync(ITelegramBotClient client, Update update, CancellationToken token, Context context)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    await MessageHandler.HandlingAsync(client, update, context);
                }
                else if(update.Type == UpdateType.CallbackQuery)
                {
                    await CallbackQueryHandler.HandlingAsync(client, update, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
