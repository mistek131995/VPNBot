using Telegram.Bot.Types.ReplyMarkups;
using VPNBot.Handler.UpdateHandler.Message.Model;

namespace VPNBot.Handler.UpdateHandler.Message.MessageBuilder.MainMenu
{
    internal class MainMenuMessage : IMessageBuilder
    {
        public static MessageModel Build()
        {
            var keyboard = new ReplyKeyboardMarkup(new List<KeyboardButton[]>()
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("Получить доступ")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Скачать приложение"),
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Сообщить об ошибке"),
                    new KeyboardButton("Вернуться назад")
                }
            })
            {
                ResizeKeyboard = true
            };

            return new MessageModel("Выберите действие", replyKeyboard: keyboard, inlineKeyboard: null);
        }
    }
}
