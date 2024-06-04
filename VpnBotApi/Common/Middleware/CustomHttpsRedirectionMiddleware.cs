using IP2LocationIOComponent;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace VpnBotApi.Common.Middleware
{
    public class CustomHttpsRedirectionMiddleware
    {
        private const int PortNotFound = -1;

        private readonly RequestDelegate _next;
        private readonly Lazy<int> _httpsPort;
        private readonly int _statusCode;

        private readonly IServerAddressesFeature? _serverAddressesFeature;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes <see cref="HttpsRedirectionMiddleware" />.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="config"></param>
        /// <param name="loggerFactory"></param>
        public CustomHttpsRedirectionMiddleware(RequestDelegate next, IOptions<HttpsRedirectionOptions> options, IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(next);
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(config);

            _next = next;
            _config = config;

            var httpsRedirectionOptions = options.Value;
            if (httpsRedirectionOptions.HttpsPort.HasValue)
            {
                _httpsPort = new Lazy<int>(httpsRedirectionOptions.HttpsPort.Value);
            }
            else
            {
                _httpsPort = new Lazy<int>(TryGetHttpsPort);
            }
            _statusCode = httpsRedirectionOptions.RedirectStatusCode;
        }

        /// <summary>
        /// Initializes <see cref="HttpsRedirectionMiddleware" />.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        /// <param name="config"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="serverAddressesFeature"></param>
        public CustomHttpsRedirectionMiddleware(RequestDelegate next, IOptions<HttpsRedirectionOptions> options, IConfiguration config, IServerAddressesFeature serverAddressesFeature) : this(next, options, config)
        {
            _serverAddressesFeature = serverAddressesFeature ?? throw new ArgumentNullException(nameof(serverAddressesFeature));
        }

        /// <summary>
        /// Invokes the HttpsRedirectionMiddleware.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            Configuration Config = new()
            {
                ApiKey = "B402E5F8EAE840F9DF2AF3F76358F80E"
            };

            IPGeolocation IPL = new(Config);

            // Lookup ip address geolocation data
            var MyTask = IPL.Lookup(context.Request.HttpContext.Connection.RemoteIpAddress.ToString());
            var MyObj = MyTask.Result;

            if (context.Request.IsHttps || MyObj["country_code"].ToString() == "IR")
            {
                return _next(context);
            }

            var port = _httpsPort.Value;
            if (port == PortNotFound)
            {
                return _next(context);
            }

            var host = context.Request.Host;
            if (port != 443)
            {
                host = new HostString(host.Host, port);
            }
            else
            {
                host = new HostString(host.Host);
            }

            var request = context.Request;
            var redirectUrl = UriHelper.BuildAbsolute(
                "https",
                host,
                request.PathBase,
                request.Path,
                request.QueryString);

            context.Response.StatusCode = _statusCode;
            context.Response.Headers.Location = redirectUrl;

            return Task.CompletedTask;
        }

        //  Returns PortNotFound (-1) if we were unable to determine the port.
        private int TryGetHttpsPort()
        {
            // The IServerAddressesFeature will not be ready until the middleware is Invoked,
            // Order for finding the HTTPS port:
            // 1. Set in the HttpsRedirectionOptions
            // 2. HTTPS_PORT environment variable
            // 3. IServerAddressesFeature
            // 4. Fail if not sets

            var nullablePort = GetIntConfigValue("HTTPS_PORT") ?? GetIntConfigValue("ANCM_HTTPS_PORT");
            if (nullablePort.HasValue)
            {
                var port = nullablePort.Value;
                return port;
            }

            if (_serverAddressesFeature == null)
            {
                return PortNotFound;
            }

            foreach (var address in _serverAddressesFeature.Addresses)
            {
                var bindingAddress = BindingAddress.Parse(address);
                if (bindingAddress.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                {
                    // If we find multiple different https ports specified, throw
                    if (nullablePort.HasValue && nullablePort != bindingAddress.Port)
                    {
                        throw new InvalidOperationException(
                            "Cannot determine the https port from IServerAddressesFeature, multiple values were found. " +
                            "Set the desired port explicitly on HttpsRedirectionOptions.HttpsPort.");
                    }
                    else
                    {
                        nullablePort = bindingAddress.Port;
                    }
                }
            }

            if (nullablePort.HasValue)
            {
                var port = nullablePort.Value;
                return port;
            }

            return PortNotFound;

            int? GetIntConfigValue(string name) =>
                int.TryParse(_config[name], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var value) ? value : null;
        }
    }
}
