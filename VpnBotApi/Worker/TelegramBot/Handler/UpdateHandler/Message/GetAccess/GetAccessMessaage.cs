using Database;
using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess
{
    public class GetAccessMessage
    {
        public static MessageModel Build(List<(int id, DateTime endDate)> access)
        {
            var buttons = access.Select(x => InlineKeyboardButton.WithCallbackData($"{x.id} от {x.endDate.ToShortDateString()}")).ToList();


            var keyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Новый доступ", "new-access")
                },
                buttons.ToArray()
            });

            return new MessageModel("Выберите доступ или создайте новый:", keyboard);
        }
    }
}
