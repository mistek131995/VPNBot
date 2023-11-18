using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.MessageHandler.GetAccess
{
    public class Response
    {
        public string Text { get; set; }
        public byte[] AccessQrCode { get; set; }
        public ReplyKeyboardMarkup ReplyKeyboard { get; set; }
    }
}
