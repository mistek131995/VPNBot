using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using VPNBot.Handler.ErrorHandler;
using VPNBot.Handler.UpdateHandler;

namespace VPNBot
{
    internal class Program
    {
        private static ITelegramBotClient botClient;
        private static ReceiverOptions receiverOptions;

        static async Task Main(string[] args)
        {
            IServiceScope scope = Service.ServiceProvider.RegisterService();

            var errorHandler = scope.ServiceProvider.GetRequiredService<ErrorHandler>();
            var updateHandler = scope.ServiceProvider.GetRequiredService<UpdateHandler>();

            botClient = new TelegramBotClient("6619723942:AAHI2FPYo2P0ESn_9D1SHiaA1m0Mb3QwKq0");

            receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message
                },

                // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
                // True - не обрабатывать, False (стоит по умолчанию) - обрабатывать
                ThrowPendingUpdates = true
            };

            using var cts = new CancellationTokenSource();

            botClient.StartReceiving(updateHandler.Handling, errorHandler.Handling, receiverOptions, cts.Token); // Запускаем бота

            var me = await botClient.GetMeAsync(); // Создаем переменную, в которую помещаем информацию о нашем боте.
            Console.WriteLine($"{me.FirstName} запущен!");

            await Task.Delay(-1);
        }
    }
}