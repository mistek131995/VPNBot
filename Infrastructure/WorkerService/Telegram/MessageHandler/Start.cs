using Infrastructure.WorkerService.Telegram.MessageHandler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static Infrastructure.WorkerService.Telegram.MessageHandler.Common.IMessageHandler;

namespace Infrastructure.WorkerService.Telegram.MessageHandler
{
    internal class Start : IMessageHandler
    {
        public Task<Result> Handle(Update update)
        {
            throw new NotImplementedException();
        }
    }
}
