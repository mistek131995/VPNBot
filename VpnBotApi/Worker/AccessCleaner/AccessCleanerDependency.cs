namespace VpnBotApi.Worker.AccessCleaner
{
    public static class AccessCleanerDependency
    {
        public static IServiceCollection AddAccessCleaner(this IServiceCollection services)
        {
            services.AddHostedService<AccessCleanerWorker>();

            return services;
        }
    }
}
