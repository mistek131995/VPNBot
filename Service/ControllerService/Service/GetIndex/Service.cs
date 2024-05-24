using Application.ControllerService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;

namespace Service.ControllerService.Service.GetIndex
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);
            var accessPositions = await repositoryProvider.AccessPositionRepository.GetAllAsync();
            var referals = await repositoryProvider.UserRepository.GetByParentIdAsync(request.UserId);

            result.Login = user.Login;
            result.Email = user.Email;
            result.RegisterDate = user.RegisterDate;
            result.EndAccessDate = user.AccessEndDate;
            result.Balance = user.Balance;
            result.ReferralLink = $"https://{configuration["Domain"]}/Register?parent={user.Guid}";


            foreach (var payment in user.Payments.Take(10))
            {
                var position = accessPositions.FirstOrDefault(x => x.Id == payment.AccessPositionId);

                result.Payments.Add(new Result.Payment()
                {
                    Id = payment.Id,
                    Date = payment.Date,
                    Range = position.Name,
                    Price = $"{position.Price} руб.",
                    State = payment.State
                });
            }

            result.Referrals = referals.Take(10)
                .Select(x => new Result.Referral()
                {
                    Login = x.Login,
                    TopupTotal = x.Payments.Sum(s => s.Amount * 0.1m) //Потому что 10%
                })
                .ToList();

            return result;
        }
    }
}
