using Database.Common;

namespace VpnBotApi.Common
{
    public interface IControllerHandler<TQuery, TResponse>
    {
        public Task<TResponse> HandlingAsync(TQuery query);
    }
}
