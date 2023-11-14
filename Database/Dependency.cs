using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class Dependency
    {
        public static ServiceCollection RegisterDatabaseDependency(this ServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];

            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
