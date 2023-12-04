using Application.TelegramBotService.Common;
using Core.Common;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.BuyAccess
{
    internal class Service(IRepositoryProvider repositoryProvider) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var accessPositions = (await repositoryProvider.AccessPositionRepository.GetAllAsync())
                .Select(x => (x, "http://lockvpn.local/"))
                .ToList();

            var result = new Result();
            result.InlineKeyboard = ButtonTemplate.GetAccessPositionsButton(accessPositions);
            result.Text = "Выберите подписку:";
            return result;
        }
    }
}
