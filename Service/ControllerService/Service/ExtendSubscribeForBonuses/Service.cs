using Application.ControllerService.Common;
using Core.Common;
using Serilog;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.ExtendSubscribeForBonuses
{
    internal class Service(IRepositoryProvider repositoryProvider, ILogger logger) : IControllerService<Request, bool>
    {
        public async Task<bool> HandlingAsync(Request request)
        {
            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) 
                ?? throw new HandledException("Пользователь не найден.");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.PositionId);

            user.SubtractBalance(accessPosition.Price);
            user.UpdateAccessEndDate(accessPosition.MonthCount);

            await repositoryProvider.UserRepository.UpdateAsync(user);
            logger.Information($"Успешное продление за бонусы. Списано - {accessPosition.Price}, пользователь - {user.Id}.");

            return true;
        }
    }
}
