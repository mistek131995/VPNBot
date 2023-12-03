namespace Application.TelegramBotService.Common
{
    public class TelegramBotServiceDispatcher(IServiceProvider serviceProvider)
    {
        public Task<TResult> GetService<TResult, TRequest>(TRequest query) where TRequest : IRequest<TResult>
        {
            var handler = (IBotService<TRequest, TResult>)serviceProvider.GetService(typeof(IBotService<TRequest, TResult>));

            return handler.HandlingAsync(query);
        }
    }
}
