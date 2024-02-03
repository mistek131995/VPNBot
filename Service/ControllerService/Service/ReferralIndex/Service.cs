using Application.ControllerService.Common;
using Core.Common;
using Microsoft.Extensions.Configuration;

namespace Service.ControllerService.Service.ReferralIndex
{
    internal class Service(IRepositoryProvider repositoryProvider, IConfiguration configuration) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(request.UserId);

            var childUsers = await repositoryProvider.UserRepository.GetByParentIdAsync(request.UserId);

            result.ReferralLink = $"https://{configuration["Domain"]}/Register?parent={user.Guid}";
            result.Referrals = childUsers
                .Select(x => new Result.Referral()
                {
                    Name = x.Login,
                    TopupTotal = x.Payments.Sum(s => s.Amount * 0.1m)
                })
                .ToList();

            return result;
        }
    }
}
