namespace VpnBotApi.Worker.TelegramBot.Common
{
    public class HandlerDispatcher(IServiceProvider provider)
    {
        private readonly IServiceProvider provider = provider;

        public Task<TResponse> BuildHandler<TResponse, TQuery>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var handler = (IHandler<TQuery, TResponse>)provider.GetService(typeof(IHandler<TQuery, TResponse>));

            return handler.HandlingAsync(query);
        }
    }
}
