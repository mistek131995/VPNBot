using Telegram.Bot.Types.ReplyMarkups;

namespace Service.TelegramBotService.Service.UnknowCommand
{
    public class Result
    {
        public string Text { get; set; }
        public ReplyKeyboardMarkup ReplyKeyboard { get; set; }
    }
}
