using Database.Common;

namespace VpnBotApi.Common
{
    public interface IControllerHandler<TResponse, TQuery>
    {
        public Task<TResponse> HandlingAsync(TQuery query);
    }
}
