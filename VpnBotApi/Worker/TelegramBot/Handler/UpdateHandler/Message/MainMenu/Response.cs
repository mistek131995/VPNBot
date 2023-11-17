using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.MainMenu
{
    public class Response
    {
        public string Text { get; set; }
        public ReplyKeyboardMarkup ReplyKeyboard { get; set; }
    }
}
