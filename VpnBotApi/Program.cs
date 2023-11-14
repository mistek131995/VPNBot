using Database;
using Microsoft.EntityFrameworkCore;
using VpnBotApi.Worker.Common;
using VpnBotApi.Worker.TelegramBot;

namespace VpnBotApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("mssql");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
            builder.Services.AddScoped<TelegramWorker>();
            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Database")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}