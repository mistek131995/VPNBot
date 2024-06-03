using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using VpnBotApi.Common.Middleware;

namespace VpnBotApi.Common.Extensions
{
    public static class CustomHttpsPolicyBuilderExtensions
    {
        /// <summary>
        /// Adds middleware for redirecting HTTP Requests to HTTPS.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance this method extends.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> for HttpsRedirection.</returns>
        public static IApplicationBuilder UseCustomHttpsPolicyBuilderExtensions(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            var serverAddressFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            if (serverAddressFeature != null)
            {
                app.UseMiddleware<CustomHttpsRedirectionMiddleware>(serverAddressFeature);
            }
            else
            {
                app.UseMiddleware<CustomHttpsRedirectionMiddleware>();
            }
            return app;
        }
    }
}
