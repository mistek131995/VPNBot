using Database;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.MainMenu
{
    public class Handler : IHandler<Query, Response>
    {
        private readonly Context context;
        public Handler(Context context) => 
            this.context = context;

        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            response.ReplyKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("Получить доступ")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Скачать приложение")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Сообщить об ошибке")
                }
            })
            {
                ResizeKeyboard = true
            };

            return response;
        }
    }
}
