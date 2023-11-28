using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Query.Index
{
    public class Handler(IRepositoryProvider repositoryProvider) : IControllerHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();

            var user = await repositoryProvider.UserRepository.GetByIdAsync(query.UserId);

            response.Login = user.Login;
            response.Email = user.Email;
            response.RegisterDate = user.RegisterDate;
            response.EndAccessDate = user.Access.EndDate;

            return response;
        }
    }
}
