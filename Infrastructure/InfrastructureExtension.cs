using Core.Common;
using Infrastructure.Common;
using Infrastructure.Database;
using Infrastructure.TelegramService;
using Infrastructure.WorkerService.Telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mssql");

            services.AddScoped<IRepositoryProvider, RepositoryProvider>();
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            services.AddScoped<ITelegramNotificationService, TelegramNotificationService>();
            services.AddHostedService<TelegramWorker>();

            return services;
        }
    }
}
