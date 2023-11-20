using Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mssql");

            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            services.AddScoped<IRepositoryProvider, RepositoryProvider>();

            return services;
        }
    }
}
