﻿using Application.ControllerService.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Service
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ControllerServiceDispatcher>();

            #region Регистрация сервисов контроллеров

            var controllerService = Assembly.GetAssembly(typeof(ControllerServiceDispatcher))
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IControllerService<,>)))
                .ToList();

            controllerService.ForEach(handler =>
            {
                var _interface = handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IControllerService<,>));

                services.AddScoped(_interface, handler);
            });

            #endregion

            return services;
        }
    }
}
