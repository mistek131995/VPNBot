using Database;
using Domain.HttpClientService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using VpnBotApi.Common;
using VpnBotApi.Common.ExceptionHandler;
using VpnBotApi.Worker.AccessCleaner;
using VpnBotApi.Worker.TelegramBot;

namespace VpnBotApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.log", rollingInterval: RollingInterval.Month)
                .CreateLogger();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
            });
            builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddTelegramBot();
            builder.Services.AddControllerHandler();
            builder.Services.AddAccessCleaner();
            builder.Services.AddHttpClientService();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<Context>();

                await dbContext.Database.MigrateAsync();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }

        public class AuthOptions
        {
            public const string ISSUER = "LockVpn.Server"; // издатель токена
            public const string AUDIENCE = "LockVpn.Client"; // потребитель токена
            const string KEY = "asfdddskjjawiejaljslockvpnasdjkasdjlajdfjha41747849347*^*&%$";   // ключ для шифрации
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}