using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace Infrastructure.WorkerService.Telegram.MessageHandler.Common
{
    internal class BaseHandler(ITelegramBotClient botClient, Update update, IRepositoryProvider repositoryProvider) : IMessageHandler
    {
        public virtual async Task Handle()
        {
            await botClient.SendTextMessageAsync(update.Message.From.Id, "Метод не был переопределен");
        }
    }
}
