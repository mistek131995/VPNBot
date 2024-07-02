using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Infrastructure.WorkerService.Telegram.MessageHandler.Common
{
    internal interface IMessageHandler
    {
        record Result(string Message, InlineKeyboardMarkup InlineKeyboard);
        Task<Result> Handle(Update update);
    }
}
