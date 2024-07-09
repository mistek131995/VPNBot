﻿using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.User.SaveNotificationSettings
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledException("Пользователь не найден");

            user.UserSetting.UseTelegramNotificationTicketMessage = request.UseTelegramNotificationTicketMessage;
            user.UserSetting.UseTelegramNotificationLoginInError = request.UseTelegramNotificationLoginInError;
            user.UserSetting.UseTelegramNotificationAboutNews = request.UseTelegramNotificationAboutNews;
            user.UserSetting.EmailNotificationTicketMessage = request.EmailNotificationTicketMessage;
            user.UserSetting.EmailNotificationLoginInError = request.EmailNotificationLoginInError;
            user.UserSetting.EmailNotificationAboutNews = request.EmailNotificationAboutNews;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
