using Telegram.Bot.Types.ReplyMarkups;

namespace Service.TelegramBotService.Service.GetAccess
{
    public class Result
    {
        public string Text { get; set; }
        public ReplyKeyboardMarkup ReplyKeyboard { get; set; }
        public byte[] QRCode { get; set; }
    }
}
