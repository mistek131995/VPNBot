using Service.ControllerService.Common;
using System.Net;

namespace VpnBotApi.Common
{
    public class ExceptionMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
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
                    Console.WriteLine(ex.Message);
                    await HandleExceptionAsync(httpContext, new HandledException("Произошла непредвиденная ошибка, мы уже работаем над ее устранением."));
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
