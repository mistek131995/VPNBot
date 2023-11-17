using VpnBotApi.Worker.TelegramBot.Common;

namespace VpnBotApi.Worker.TelegramBot.Handler.UpdateHandler.Message.MainMenu
{
    public class MessageHandler : IHandler<Query, MessageModel>
    {
        public async Task<MessageModel> HandlingAsync(Query query)
        {

            Console.WriteLine("Work");

            return new MessageModel();
        }
    }
}
