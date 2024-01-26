﻿using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.ChangePassword
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId)
                ?? throw new HandledExeption("Пользователь не найден.");

            if (user.Password != request.OldPassword)
                throw new HandledExeption("Введен неверный пароль.");

            user.Password = request.Password;

            await repositoryProvider.UserRepository.UpdateAsync(user);

            return true;
        }
    }
}
