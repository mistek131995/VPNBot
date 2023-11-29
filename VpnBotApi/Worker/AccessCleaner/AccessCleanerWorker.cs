
namespace VpnBotApi.Worker.AccessCleaner
{
    public class AccessCleanerWorker : IHostedService, IDisposable
    {
        private Timer? _timer = null;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, 0,
               1000);

            return Task.CompletedTask;
        }

        public static void DoWork(object obj)
        {
            Console.WriteLine("Work");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
