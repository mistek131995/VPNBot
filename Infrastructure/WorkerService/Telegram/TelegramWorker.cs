using Core.Common;
using Infrastructure.WorkerService.Telegram.MessageHandler;
using Infrastructure.WorkerService.Telegram.MessageHandler.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Infrastructure.WorkerService.Telegram
{
    internal class TelegramWorker(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var repositoryProvider = scope.ServiceProvider.GetRequiredService<IRepositoryProvider>();
                var settings = await repositoryProvider.SettingsRepositroy.GetSettingsAsync();

                var botClient = new TelegramBotClient(settings.TelegramToken);
                var receiverOptions = new ReceiverOptions // Также присваем значение настройкам бота
                {
                    AllowedUpdates = new[] // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
                    {
                        UpdateType.Message, // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
                        UpdateType.CallbackQuery
                    },
                };

                botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions: receiverOptions);
            }
        }

        public void UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                IMessageHandler handler;

                if(update.Message.Text == "/start")
                {
                    handler = new Start();
                    handler.Handle(update);
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {

            }
            else
            {
                throw new Exception("Неизвестный тип сообщения");
            }
        }

        public void ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {

        }
    }
}
