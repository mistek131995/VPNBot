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
                ?? throw new HandledExeption("Пользователь не найден.");

            var accessPosition = await repositoryProvider.AccessPositionRepository.GetByIdAsync(request.PositionId);

            if (user.Balance < accessPosition.Price)
                throw new HandledExeption("Недостаточно бонусов для оплаты подписки.");

            if (user.AccessEndDate == null || user.AccessEndDate < DateTime.Now)
            {
                user.AccessEndDate = DateTime.Now.AddMonths(accessPosition.MonthCount).Date;
            }
            else
            {
                user.AccessEndDate = user.AccessEndDate?.AddMonths(accessPosition.MonthCount).Date;
            }

            user.Balance -= accessPosition.Price;

            await repositoryProvider.UserRepository.UpdateAsync(user);
            logger.Information($"Успешное продление за бонусы. Списано - {accessPosition.Price}, пользователь - {user.Id}.");

            return true;
        }
    }
}
