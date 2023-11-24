using Database.Common;
using Telegram.Bot.Types.ReplyMarkups;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.AccountManagment
{
    public class Handler(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(query.TelegramUserId);
            var domain = configuration["Domain"];

            var inlineKeyboard = new List<InlineKeyboardButton[]>();

            //Если подписка не истекла
            if (user.Access.EndDate.Date > DateTime.Now.Date)
            {
                response.Text = $"Ваша подписка активна до {user.Access.EndDate.ToShortDateString()}. Выберите действие:";
                inlineKeyboard.Add([
                    InlineKeyboardButton.WithCallbackData("Получить QR код", "getQrCode"),
                ]);
            }
            else
            {
                response.Text = $"Ваша подписка закончилась {user.Access.EndDate.ToShortDateString()}. Выберите действие:";
            }

            //Общие кнопки 
            inlineKeyboard.Add([
                InlineKeyboardButton.WithUrl("Личный кабинет", $"http://{domain}/login?TelegramUserId={user.TelegramUserId}&Guid={user.Access.Guid}"),
                InlineKeyboardButton.WithUrl("Подписка", $"http://{domain}/subscribeManagement?TelegramUserId={user.TelegramUserId}&Guid={user.Access.Guid}")
            ]);

            response.InlineKeyboard = new InlineKeyboardMarkup(inlineKeyboard);

            return response;
        }
    }
}
