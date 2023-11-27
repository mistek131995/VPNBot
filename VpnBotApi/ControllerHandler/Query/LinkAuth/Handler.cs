using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Query.LinkAuth
{
    public class Handler(IRepositoryProvider repositoryProvider) : IControllerHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAndAccessGuidAsync(query.TelegramUserId, query.Guid);

            if (user == null)
                response.State = Response.UserState.NotFound;
            else if (string.IsNullOrEmpty(user.Password))
                response.State = Response.UserState.NeedSetPassword;
            else
                response.State = Response.UserState.Ready;

            return response;
        }
    }
}
