﻿using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.DeleteLog
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var log = await repositoryProvider.LogRepository.GetByIdAsync(request.Id) 
                ?? throw new Exception("Лог не найден.");

            await repositoryProvider.LogRepository.DeleteByIdAsync(request.Id);

            return true;
        }
    }
}
