using Database;
using Telegram.Bot.Types.ReplyMarkups;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.GetAccess
{
    public class GetAccessMessaage
    {
        public static MessageModel Build(List<(int id, DateTime endDate)> access)
        {
            var buttons = access.Select(x => InlineKeyboardButton.WithCallbackData($"{x.id} от {x.endDate.ToShortDateString()}")).ToList();
            buttons.Insert(0, InlineKeyboardButton.WithCallbackData("Новый доступ"));


            var keyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
            {
                buttons.ToArray()
            });

            return new MessageModel("Выберите доступ или создайте новый:", keyboard);
        }
    }
}
