using Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VpnBotApi.Common;
using VpnBotApi.Common.ExceptionHandler;
using VpnBotApi.Worker.Common;
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddTelegramBot();
            builder.Services.AddControllerHandler();


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
    }
}