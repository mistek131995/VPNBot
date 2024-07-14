using Core.Common;
using Infrastructure.TelegramService;
using Service.ControllerService.Common;
using System.Net;

namespace VpnBotApi.Common.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        public async Task InvokeAsync(HttpContext httpContext, IRepositoryProvider repositoryProvider, ITelegramNotificationService telegramNotificationService)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                if (typeof(HandledException) == ex.GetType())
                {
                    await HandleExceptionAsync(httpContext, ex);

                    if (((HandledException)ex).WriteToLog)
                    {
                        logger.Error(ex, ex.StackTrace);
                    }
                }
                else
                {
                    logger.Error(ex, ex.StackTrace);
                    await HandleExceptionAsync(httpContext, new HandledException("Произошла непредвиденная ошибка, мы уже работаем над ее устранением."));

                    var admins = await repositoryProvider.UserRepository.GetAllAdminsAsync();

                    foreach (var admin in admins)
                    {
                        await telegramNotificationService.AddText(ex.Message).SendNotificationAsync(admin.TelegramUserId);
                    }
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;

            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new { exception.Message, exception.StackTrace }));
        }
    }
}
