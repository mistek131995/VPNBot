using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VPNBot.Handler.UpdateHandler.Message;

namespace VPNBot.Handler.UpdateHandler
{
    internal class UpdateHandler
    {
        public async Task Handling(ITelegramBotClient client, Update update, CancellationToken token)
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
