using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.Help
{
    public class Response
    {
        public string Text { get; set; }
        public InlineKeyboardMarkup InlineKeyboard {  get; set; }
    }
}
