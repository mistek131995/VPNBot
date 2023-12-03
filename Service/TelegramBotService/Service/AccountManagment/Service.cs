using Application.TelegramBotService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.AccountManagment
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId)
                ?? throw new Exception("Пользователь не найден.");

            if (user.Access.EndDate.Date <= DateTime.Now.Date)
            {
                result.Text = "Ваша подписка закончилась. Продлите подписку, чтобы продолжить использовать VPN.";
            }
            else
            {
                result.Text = $"Ваша подписка активна до {user.Access.EndDate.ToShortDateString()}.";
            }

            result.InlineKeyboard = ButtonTemplate.GetAccountManegment(user.Access.EndDate, configuration["Domain"], user.TelegramUserId, user.Access.Guid);

            return result;
        }
    }
}
