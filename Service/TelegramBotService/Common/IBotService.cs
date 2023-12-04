namespace Application.TelegramBotService.Common
{
    internal interface IBotService<TRequest, TResult>
    {
        public Task<TResult> HandlingAsync(TRequest request);
    }
}
