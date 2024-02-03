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

            result.ReferralLink = $"https://{configuration["Domain"]}/User/Register?parent={user.Guid}";
            result.Referrals = new List<Result.Referral>();

            return result;
        }
    }
}
