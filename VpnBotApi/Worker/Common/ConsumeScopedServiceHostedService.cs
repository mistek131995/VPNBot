using VpnBotApi.Worker.TelegramBot;

namespace VpnBotApi.Worker.Common
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        public ConsumeScopedServiceHostedService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider
                    .GetRequiredService<TelegramWorker>();

                await scopedProcessingService.StartBotAsync(stoppingToken);
            }
        }
    }
}
