using Database.Common;

namespace VpnBotApi.Worker.AccessCleaner
{
    public class AccessCleanerWorker(IServiceProvider serviceProvider) : IHostedService, IDisposable
    {
        private Timer timer;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(DoWork, null, 0, 86400000);

            return Task.CompletedTask;
        }

        public async void DoWork(object obj)
        {
            using(IServiceScope scope = serviceProvider.CreateScope())
            {
                var repositoryProvider = scope.ServiceProvider.GetService<IRepositoryProvider>();

                var deprecatedAccess = await repositoryProvider.AccessRepository.GetDeprecatedAccessAsync(DateTime.Now.AddDays(-7));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
