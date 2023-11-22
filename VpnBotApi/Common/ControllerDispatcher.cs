namespace VpnBotApi.Common
{
    public class ControllerDispatcher(IServiceProvider serviceProvider)
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public Task<TResponse> BuildHandler<TResponse, TQuery>(TQuery query) where TQuery : IControllerQuery<TResponse>
        {
            var handler = (IControllerHandler<TQuery, TResponse>)serviceProvider.GetService(typeof(IControllerHandler<TQuery, TResponse>));

            return handler.HandlingAsync(query);
        }
    }
}
