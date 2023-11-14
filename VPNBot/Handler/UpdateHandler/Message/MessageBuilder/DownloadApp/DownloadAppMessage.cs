using Telegram.Bot.Types.ReplyMarkups;
using VPNBot.Handler.UpdateHandler.Message.Model;

namespace VPNBot.Handler.UpdateHandler.Message.MessageBuilder.DownloadApp
{
    internal class DownloadAppMessage : IMessageBuilder
    {
        public static MessageModel Build()
        {
            var keyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Android", "https://play.google.com/store/apps/details?id=com.v2ray.ang"),
                    InlineKeyboardButton.WithUrl("IOS", "https://apps.apple.com/us/app/streisand/id6450534064"),
                    InlineKeyboardButton.WithUrl("Windows", "https://github.com/MatsuriDayo/nekoray/releases")
                }
            });

            return new MessageModel("Выберите ОС устройства:", null, keyboard);
        }
    }
}
