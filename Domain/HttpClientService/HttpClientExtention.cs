using Microsoft.Extensions.DependencyInjection;

namespace Domain.HttpClientService
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
