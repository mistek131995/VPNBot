using System.Reflection;
using VPNBot.Handler.ErrorHandler;
using VPNBot.Handler.UpdateHandler;
using VPNBot.Handler.UpdateHandler.Message;
using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot
{
    public static class TelegramBotDependency
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddScoped<TelegramWorker>();
            services.AddScoped<ErrorHandler>();
            services.AddScoped<UpdateHandler>();
            services.AddScoped<MessageHandler>();

            services.AddScoped<HandlerDispatcher>();

            #region Регистрация обработчиков

            var handlers = Assembly.GetAssembly(typeof(TelegramBotDependency))
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>)))
                .ToList();

            handlers.ForEach(handler =>
            {
                var _interface = handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>));

                services.AddScoped(_interface, handler);
            });

            #endregion

            return services;
        }
    }
}
