namespace VpnBotApi.Worker.TelegramBot
{
    public static class TelegramBotDependency
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddScoped<TelegramWorker>();
            services.AddScoped<ErrorHandler.ErrorHandler>();
            services.AddScoped<UpdateHandler.UpdateHandler>();
            services.AddScoped<MessageHandler.MessageHandler>();
            services.AddScoped<CallbackQueryHandler.CallbackQueryHandler>();

            return services;
        }
    }
}
