using Core.Common;
using Infrastructure.Common;
using Infrastructure.Database;
using Infrastructure.HttpClientService;
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
            services.AddScoped<IHttpClientService, HttpClientService.HttpClientService>();
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));

            return services;
        }
    }
}
