using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.HttpClientService
{
    public static class HttpClientExtention
    {
        public static IServiceCollection AddHttpClientService(this IServiceCollection services)
        {
            services.AddScoped<IHttpClientService, HttpClientService>();

            return services;
        }
    }
}
