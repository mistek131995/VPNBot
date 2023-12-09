using Serilog;
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
                logger.Error(ex.StackTrace ?? ex.Message);
                await HandleExceptionAsync(httpContext, ex);
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
