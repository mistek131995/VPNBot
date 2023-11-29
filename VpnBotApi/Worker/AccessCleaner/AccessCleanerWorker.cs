using Database.Common;
using Database.Model;
using Domain.HttpClientService;

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
                var httpClientService = scope.ServiceProvider.GetService<IHttpClientService>();

                var deprecatedAccess = await repositoryProvider.AccessRepository.GetDeprecatedAccessAsync(DateTime.Now.AddDays(-7));

                if (deprecatedAccess.Any())
                {
                    var groupedDeprecatedAccesses = deprecatedAccess
                        .GroupBy(x => new { x.VpnServer.Ip, x.VpnServer.Port })
                        .Select(x => new
                        {
                            x.Key.Ip,
                            x.Key.Port,
                            Guids = x.Select(g => g.Guid).ToList()
                        })
                        .ToList();

                    var allSuccessDeleteGuids = new List<Guid>();

                    foreach(var access in groupedDeprecatedAccesses)
                    {
                        var successDeleteGuids = await httpClientService.DeleteInboundUserAsync(access.Guids, access.Ip, access.Port);
                        allSuccessDeleteGuids.AddRange(successDeleteGuids);
                    }

                    deprecatedAccess = deprecatedAccess.Where(x => allSuccessDeleteGuids.Contains(x.Guid)).ToList();

                    deprecatedAccess.ForEach(access =>
                    {
                        access.IsDeprecated = true;
                    });

                    await repositoryProvider.AccessRepository.UpdateManyAsync(deprecatedAccess);
                }
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
