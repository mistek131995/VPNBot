using Database;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VPNBot.Handler.UpdateHandler.Message;

namespace VPNBot.Handler.UpdateHandler
{
    internal class UpdateHandler
    {
        public static async Task Handling(ITelegramBotClient client, Update update, CancellationToken token, Context context)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    await MessageHandler.Handling(client, update);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
