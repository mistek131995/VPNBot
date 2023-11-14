using Telegram.Bot.Types.ReplyMarkups;

namespace VPNBot.Handler.UpdateHandler.Message.Model
{
    public class MessageModel
    {
        public string Text { get; set; }
        public ReplyKeyboardMarkup ReplyKeyboard { get; set; }
        public InlineKeyboardMarkup InlineKeyboard { get; set; }

        public MessageModel(string text, ReplyKeyboardMarkup replyKeyboard, InlineKeyboardMarkup inlineKeyboard) 
        { 
            Text = text;
            ReplyKeyboard = replyKeyboard;
            InlineKeyboard = inlineKeyboard;
        }
    }
}
