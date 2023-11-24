using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.AccountManagment
{
    public class Response
    {
        public InlineKeyboardMarkup InlineKeyboard {  get; set; }
        public string Text { get; set; }
    }
}
