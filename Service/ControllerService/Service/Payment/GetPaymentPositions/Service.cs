using Application.ControllerService.Common;
using Core.Common;
using Service.ControllerService.Common;

namespace Service.ControllerService.Service.Payment.GetPaymentPositions
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId) ??
                throw new HandledExeption("Пользователь не найден");

            result.EndAccessDate = user.AccessEndDate < DateTime.Now ? DateTime.Now : user.AccessEndDate;
            result.AccessPositions = (await repositoryProvider.AccessPositionRepository.GetAllAsync()).Select(x => new Result.AccessPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                MonthCount = x.MonthCount
            }).ToList();

            return result;
        }
    }
}
