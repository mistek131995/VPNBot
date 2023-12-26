using Core.Common;
using Infrastructure.HttpClientService;

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
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var repositoryProvider = scope.ServiceProvider.GetService<IRepositoryProvider>();
                var httpClientService = scope.ServiceProvider.GetService<IHttpClientService>();

                var users = (await repositoryProvider.UserRepository.GetByAccessDateRangeAsync(DateTime.MinValue, DateTime.Now.AddDays(-7)))
                    .Where(x => !x.Access.IsDeprecated)
                    .ToList();

                var vpnServers = await repositoryProvider.VpnServerRepository.GetAllAsync();

                if (users.Any())
                {
                    var groupedDeprecatedAccesses = users
                        .GroupBy(x => x.Access.VpnServerId)
                        .Select(x => new
                        {
                            x.Key,
                            Guids = x.Select(g => g.Access.Guid).ToList()
                        })
                        .ToList();

                    var allSuccessDeleteGuids = new List<Guid>();

                    foreach (var access in groupedDeprecatedAccesses)
                    {
                        var vpnServer = vpnServers.FirstOrDefault(x => x.Id == access.Key);

                        try
                        {
                            var successDeleteGuids = await httpClientService.DeleteInboundUserAsync(access.Guids, vpnServer);
                            allSuccessDeleteGuids.AddRange(successDeleteGuids);
                        }catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    users = users
                        .Where(x => allSuccessDeleteGuids.Contains(x.Access.Guid))
                        .ToList();

                    users.ForEach(access =>
                    {
                        access.Access.IsDeprecated = true;
                    });

                    await repositoryProvider.UserRepository.UpdateManyAsync(users);

                    //Перерасчет активных пользователей
                    var activeUsersCounts = (await repositoryProvider.UserRepository.GetAllWithActiveAccessAsync())
                        .GroupBy(x => x.Access.VpnServerId)
                        .Select(x => new {
                            VpnServerId = x.Key,
                            ActiveUserCount = x.Count()
                        }).ToList();

                    foreach (var vpnServer in vpnServers)
                    {
                        var activeUsersCount = activeUsersCounts.FirstOrDefault(x => x.VpnServerId == vpnServer.Id);

                        vpnServer.UserCount = activeUsersCount?.ActiveUserCount ?? 0;
                    }

                    await repositoryProvider.VpnServerRepository.UpdateManyAsync(vpnServers);
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
