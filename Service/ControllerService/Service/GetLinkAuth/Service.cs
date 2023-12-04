using Application.ControllerService.Common;
using Core.Common;

namespace Service.ControllerService.Service.GetLinkAuth
{
    internal class Service(IRepositoryProvider repositoryProvider) : IControllerService<Request, Result>
    {
        public async Task<Result> HandlingAsync(Request request)
        {
            var result = new Result();
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(request.TelegramUserId, request.Guid);

            if (user == null)
                result.State = Result.UserState.NotFound;
            else if (string.IsNullOrEmpty(user.Password))
                result.State = Result.UserState.NeedSetPassword;
            else
                result.State = Result.UserState.Ready;

            return result;
        }
    }
}
