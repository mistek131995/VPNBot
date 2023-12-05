using Application.TelegramBotService.Common;
using Core.Common;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.BuyAccess
{
    internal class Service(IRepositoryProvider repositoryProvider) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId)
                ?? throw new Exception("Пользователь не найден.");

            var accessPositions = (await repositoryProvider.AccessPositionRepository.GetAllAsync())
                .Select(x => (x, $"https://pay.freekassa.ru/?m=42964&oa={x.Price}&currency=RUB&o={user.Id}&s=79126779fcff62a23e1b81b8997bf3ed"))
                .ToList();

            var result = new Result();
            result.InlineKeyboard = ButtonTemplate.GetAccessPositionsButton(accessPositions);
            result.Text = "Выберите подписку:";
            return result;
        }
    }
}
