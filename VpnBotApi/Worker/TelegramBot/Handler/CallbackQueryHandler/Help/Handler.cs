using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.Help
{
    public class Handler : IHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            response.Text = "Выберите раздел:";

            response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Сообщить об ошибке", ""),
                    InlineKeyboardButton.WithUrl("Инструкция по приложениям", "")
                }
            });

            return response;
        }
    }
}
