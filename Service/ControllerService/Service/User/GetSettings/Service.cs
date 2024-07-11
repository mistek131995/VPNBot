using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.GetSettings
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) 
                ?? throw new HandledException("Пользователь не найден");

            result.Email = user.Email;
            result.TelegramId = user.TelegramUserId;

            result.UseTelegramNotificationAboutNews = user.UserSetting.UseTelegramNotificationAboutNews;
            result.UseTelegramNotificationLoginInError = user.UserSetting.UseTelegramNotificationLoginInError;
            result.UseTelegramNotificationTicketMessage = user.UserSetting.UseTelegramNotificationTicketMessage;

            result.UseEmailNotificationTicketMessage = user.UserSetting.UseEmailNotificationTicketMessage;
            result.UseEmailNotificationAboutNews = user.UserSetting.UseEmailNotificationAboutNews;
            result.UseEmailNotificationLoginInError = user.UserSetting.UseEmailNotificationLoginInError;

            return result;
        }
    }
}
