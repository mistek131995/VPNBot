using Core.Common;
using Infrastructure.WorkerService.Telegram.MessageHandler.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
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
                await botClient.SendTextMessageAsync(
                    chatId: update.Message.From.Id, 
                    text: "Это вспомогательный бот нашего проекта, из него вы смоежете получать срочные новости и оповещения. " +
                    "Бот призван упростить коммуникацию с нашими пользователями. " +
                    "Для получения уведомлений, необходимо привзяать аккаунт", 
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("Привязать аккаунт", $"https://lockvpn.me/?telegramId={update.Message.From.Id}")));
            }
            else
            {
                await botClient.SendTextMessageAsync(update.Message.From.Id, "Пользователь найден");
            }
        }

    }
}
