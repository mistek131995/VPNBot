using Database;
using Database.Common;
using Database.Model;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.MainMenu
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
        private readonly IRepositoryProvider provider = provider;
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            //Получаем пользователя
            var user = await provider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId);

            //Если его нет, регистрируем
            if (user == null)
            {
                await provider.UserRepository.AddAsync(new User()
                {
                    TelegramUserId = query.TelegramUserId
                });
            }

            response.Text = "Выберите действие.";

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
