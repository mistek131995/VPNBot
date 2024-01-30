using Core.Common;

namespace VpnBotApi.Common
{
    public class AppVersionMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        public async Task InvoiceAsync(HttpContext context, IRepositoryProvider repositoryProvider)
        {
            var androidApp = await repositoryProvider.FileRepository.GetByTagAsync("android_app");

            if(androidApp != null)
            {
                context.Response.Headers.Append("androidVersion", androidApp.Version);
                context.Response.Headers.Append("androidLink", $"https://{configuration["Domain"]}/files/{androidApp.Tag}/{androidApp.Name}");
            }
        }
    }
}
