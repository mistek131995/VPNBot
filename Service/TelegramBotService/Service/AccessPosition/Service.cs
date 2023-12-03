using Application.TelegramBotService.Common;
using Core.Common;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.AccessPosition
{
    internal class Service(IRepositoryProvider repositoryProvider) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var accessPositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            var result = new Result();
            result.InlineKeyboard = ButtonTemplate.GetAccessPositionsButton(accessPositions);
            result.Text = "Выберите подписку:";
            return result;
        }
    }
}
