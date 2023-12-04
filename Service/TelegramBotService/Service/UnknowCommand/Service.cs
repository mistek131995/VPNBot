using Application.TelegramBotService.Common;
using Core.Common;
using Service.TelegramBotService.Common;

namespace Service.TelegramBotService.Service.UnknowCommand
{
    internal class Service(IRepositoryProvider repositoryProvider) : IBotService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(request.TelegramUserId);

            if (user.TelegramChatId == 0)
            {
                user.TelegramChatId = request.TelegramChatId;
                await repositoryProvider.UserRepository.UpdateAsync(user);
            }

            result.Text = "Команда не распознана, выберите действие из меню.";
            result.ReplyKeyboard = ButtonTemplate.GetMainMenuButton();

            return result;
        }
    }
}
