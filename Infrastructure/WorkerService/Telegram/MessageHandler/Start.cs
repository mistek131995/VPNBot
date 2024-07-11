using Core.Common;
using Infrastructure.WorkerService.Telegram.MessageHandler.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.WorkerService.Telegram.MessageHandler
{
    internal class Start(ITelegramBotClient botClient, Update update, IRepositoryProvider repositoryProvider) : BaseHandler(botClient, update, repositoryProvider)
    {
        public override async Task Handle()
        {
            var user = await repositoryProvider.UserRepository.GetByTelegramUserIdAsync(update.Message.From.Id);

            if (user == null)
            {
                var message = update.Message.Entities.FirstOrDefault();


                await botClient.SendTextMessageAsync(
                    chatId: update.Message.From.Id, 
                    text: "Это вспомогательный бот нашего проекта, из него вы смоежете получать срочные новости и оповещения. " +
                    "Бот призван упростить коммуникацию с нашими пользователями. " +
                    $"Для привязки телеграма к вашему аккаунту, скопируйте и вставьте код в настройках личного кабинета - <code>{update.Message.From.Id}</code>", parseMode: ParseMode.Html);
            }
            else
            {

                await botClient.SendTextMessageAsync(update.Message.From.Id, "Ваш телеграм уже привязан к аккаунту.");
            }
        }

    }
}
