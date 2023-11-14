using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.MainMenu
{
    internal class MainMenuMessage
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

            return new MessageModel("Выберите действие", keyboard);
        }
    }
}
