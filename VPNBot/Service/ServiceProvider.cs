using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VPNBot.Handler.ErrorHandler;
using VPNBot.Handler.UpdateHandler;

namespace VPNBot.Service
{
    internal class ServiceProvider
    {
        internal static IServiceScope RegisterService()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddScoped<ErrorHandler>();
            services.AddScoped<UpdateHandler>();
            services.AddSingleton<IConfiguration>(configuration);
            services.RegisterDatabaseDependency(configuration);


            return services.BuildServiceProvider(true).CreateScope();
        }
    }
}
