using Database.Common;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.AccessPositions
{
    public class Handler(IRepositoryProvider repositoryProvider) : IHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            response.InlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>());

            var accessPositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            var positionArrayList = new List<InlineKeyboardButton[]>();

            foreach (var accessPosition in accessPositions)
            {
                //Добавляем массив позиций в список массивов (каждая с новой строки)
                positionArrayList.Add([InlineKeyboardButton.WithCallbackData($"{accessPosition.Name} за {accessPosition.Price}руб.", $"accessPosition|{accessPosition.Id}") ]);
            }

            response.InlineKeyboard = positionArrayList.ToArray();
            response.Text = "Выберите срок действия подписки:";

            return response;
        }
    }
}
