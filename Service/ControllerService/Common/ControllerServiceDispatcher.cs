namespace Application.ControllerService.Common
{
    public class ControllerServiceDispatcher(IServiceProvider serviceProvider)
    {
        public Task<TResponse> GetService<TResponse, TRequest>(TRequest query) where TRequest : IRequest<TResponse>
        {
            var handler = (IControllerService<TRequest, TResponse>)serviceProvider.GetService(typeof(IControllerService<TRequest, TResponse>));

            return handler.HandlingAsync(query);
        }
    }
}
