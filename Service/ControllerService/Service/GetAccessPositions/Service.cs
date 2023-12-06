﻿using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetAccessPositions
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            result.AccessPositions = (await repositoryProvider.AccessPositionRepository.GetAllAsync()).Select(x => new Result.AccessPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
            }).ToList();

            return result;
        }
    }
}
