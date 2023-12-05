namespace Application.ControllerService.Common
{
    public class ControllerServiceDispatcher(IServiceProvider serviceProvider)
    {
        public Task<TResult> GetService<TResult, TRequest>(TRequest query) where TRequest : IRequest<TResult>
        {
            var handler = (IControllerService<TRequest, TResult>)serviceProvider.GetService(typeof(IControllerService<TRequest, TResult>));

            return handler.HandlingAsync(query);
        }
    }
}
