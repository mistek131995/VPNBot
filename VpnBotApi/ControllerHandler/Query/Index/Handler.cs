using Database.Common;
using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Query.Index
{
    public class Handler(IRepositoryProvider repositoryProvider) : IControllerHandler<Query, Response>
    {
        public async Task<Response> HandlingAsync(Query query)
        {
            var response = new Response();


            return response;

            //var response = await repositoryProvider.UserRepository.fi
        }
    }
}
