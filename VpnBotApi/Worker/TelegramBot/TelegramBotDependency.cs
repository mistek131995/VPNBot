﻿using System.Reflection;
using VPNBot.Handler.ErrorHandler;
using VPNBot.Handler.UpdateHandler;
using VpnBotApi.Worker.TelegramBot.Common;
using VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler;
using VpnBotApi.Worker.TelegramBot.Handler.MessageHandler;
using VpnBotApi.Worker.TelegramBot.WebClientRepository;

namespace VpnBotApi.Worker.TelegramBot
{
    public static class TelegramBotDependency
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddScoped<TelegramWorker>();
            services.AddTransient<ErrorHandler>();
            services.AddTransient<UpdateHandler>();
            services.AddTransient<MessageHandler>();
            services.AddTransient<CallbackQueryHandler>();
            services.AddScoped<TelegramBotWebClient>();

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
