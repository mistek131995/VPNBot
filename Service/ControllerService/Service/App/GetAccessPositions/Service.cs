using Application.ControllerService.Common;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ControllerService.Service.App.GetAccessPositions
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, List<Result>>
    {
        public async Task<List<Result>> HandlingAsync(Request request)
        {
            var accessPositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            return accessPositions.Select(x => new Result()
            {
                Id = x.Id,
                GooglePlayIdentifier = x.GooglePlayIdentifier,
            }).ToList();
        }
    }
}
