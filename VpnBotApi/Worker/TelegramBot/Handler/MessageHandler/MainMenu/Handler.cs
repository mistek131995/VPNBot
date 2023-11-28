using Database.Common;
using Database.Model;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.MainMenu
{
    public class Handler(IRepositoryProvider provider) : IHandler<Query, Response>
    {
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
                    TelegramUserId = query.TelegramUserId,
                    TelegramChatId = query.TelegramChatId,
                    RegisterDate = DateTime.Now,
                });
            }

            response.Text = "Выберите действие.";

            response.ReplyKeyboard = new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("Получить доступ")
                }
            })
            {
                ResizeKeyboard = true
            };

            return response;
        }
    }
}
