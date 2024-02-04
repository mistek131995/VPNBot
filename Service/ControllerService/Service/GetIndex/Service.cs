using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetIndex
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);
            var accessPositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();

            result.Login = user.Login;
            result.Email = user.Email;
            result.RegisterDate = user.RegisterDate;
            result.EndAccessDate = user.AccessEndDate;
            result.Balance = user.Balance;

            if (user.Payments != null && user.Payments.Any())
            {
                foreach (var payment in user.Payments)
                {
                    var position = accessPositions.FirstOrDefault(x => x.Id == payment.AccessPositionId);

                    result.Payments.Add(new Result.Payment()
                    {
                        Id = payment.Id,
                        Date = payment.Date,
                        Range = $"{position.MonthCount} месяц.",
                        Price = $"{position.Price} руб."
                    });
                }
            }

            return result;
        }
    }
}
