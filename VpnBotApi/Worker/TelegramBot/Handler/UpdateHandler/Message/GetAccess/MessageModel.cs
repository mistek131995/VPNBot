using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess
{
    public class MessageModel
    {
        public string Text { get; set; }
        public InlineKeyboardMarkup InlineKeyboard { get; set; }

        public MessageModel(string text, InlineKeyboardMarkup inlineKeyboard) 
        { 
            Text = text;
            InlineKeyboard = inlineKeyboard;
        }
    }
}
