using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.WorkerService.Telegram.MessageHandler.Common
{
    internal interface IMessageHandler
    {
        Task Handle();
    }
}
