using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.App.GetSubscribeModal
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);
            var accessPositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            result.SubscribeType = user.SubscribeType;
            result.Subscribes = accessPositions.Select(x => new Result.Subscribe()
            {
                Id = x.Id,
                GooglePlayIdentifier = x.GooglePlayIdentifier,
            }).ToList();

            return result;
        }
    }
}
