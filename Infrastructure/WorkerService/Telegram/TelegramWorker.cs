using Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Infrastructure.WorkerService.Telegram
{
    internal class TelegramWorker(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var repositoryProvider = scope.ServiceProvider.GetRequiredService<IRepositoryProvider>();
                var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

                var botClient = new TelegramBotClient(settings.TelegramToken);

                //botClient.StartReceiving
            }
        }
    }
}
