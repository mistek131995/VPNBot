namespace VpnBotApi.Worker.TelegramBot.Common
{
    public interface IHandler<TQuery, TResponse>
    {
        public Task<TResponse> HandlingAsync(TQuery query);
    }
}
