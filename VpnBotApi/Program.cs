using Infrastructure;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Service;
using System.Text;
using VpnBotApi.Common;
using VpnBotApi.Worker.AccessCleaner;
using VpnBotApi.Worker.TelegramBot;

namespace VpnBotApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var jwtOptions = builder.Configuration.GetSection("Jwt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.log", rollingInterval: RollingInterval.Month)
                .CreateLogger();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
            });
            builder.Services.AddHostedService<ScopedHostedService>();
            builder.Services.AddTelegramBot();
            builder.Services.AddAccessCleaner();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddService();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // ���������, ����� �� �������������� �������� ��� ��������� ������
                        ValidateIssuer = true,
                        // ������, �������������� ��������
                        ValidIssuer = jwtOptions["ISSUER"],
                        // ����� �� �������������� ����������� ������
                        ValidateAudience = true,
                        // ��������� ����������� ������
                        ValidAudience = jwtOptions["AUDIENCE"],
                        // ����� �� �������������� ����� �������������
                        ValidateLifetime = true,
                        // ��������� ����� ������������
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["KEY"])),
                        // ��������� ����� ������������
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
    }
}