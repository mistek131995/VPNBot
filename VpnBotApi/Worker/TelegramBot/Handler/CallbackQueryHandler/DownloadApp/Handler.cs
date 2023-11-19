using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.DownloadApp
{
    public class Handler : IHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            response.Text = "Выберите ОС:";

            response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Android", "https://play.google.com/store/apps/details?id=com.v2ray.ang"),
                    InlineKeyboardButton.WithUrl("IOS", "https://apps.apple.com/us/app/streisand/id6450534064")
                }
            });

            return response;
        }
    }
}
