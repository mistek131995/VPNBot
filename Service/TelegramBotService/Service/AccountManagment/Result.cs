using Telegram.Bot.Types.ReplyMarkups;

namespace Service.TelegramBotService.Service.AccountManagment
{
    public class Result
    {
        public string Text { get; set; }
        public InlineKeyboardMarkup InlineKeyboard { get; set; }
    }
}
