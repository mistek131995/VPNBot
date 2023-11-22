using System.Reflection;

namespace VpnBotApi.Common
{
    public static class ControllerHandlerExtension
    {
        public static IServiceCollection AddControllerHandler(this IServiceCollection service)
        {

            service.AddScoped<ControllerDispatcher>();

            #region Регистрация обработчиков для контроллеров

            var handlers = Assembly.GetAssembly(typeof(ControllerHandlerExtension))
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IControllerHandler<,>)))
            .ToList();

            handlers.ForEach(handler =>
            {
                var _interface = handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IControllerHandler<,>));

                service.AddScoped(_interface, handler);
            });

            #endregion

            return service;
        }
    }
}
