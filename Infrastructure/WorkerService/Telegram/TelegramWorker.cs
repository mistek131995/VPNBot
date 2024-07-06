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
    internal class TelegramWorker : BackgroundService
    {
        private IRepositoryProvider _repositoryProvider;
        private IServiceScopeFactory _serviceScopeFactory;

        public TelegramWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            var scope = serviceScopeFactory.CreateScope();
            _repositoryProvider = scope.ServiceProvider.GetRequiredService<IRepositoryProvider>();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var settings = await _repositoryProvider.SettingsRepositroy.GetSettingsAsync();

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

        public void UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                IMessageHandler handler;

                if(update.Message.Text == "/start")
                {
                    handler = new Start(botClient, update, _repositoryProvider);
                    handler.Handle();
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
