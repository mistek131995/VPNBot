using VPNBot.Handler.UpdateHandler.Message.Model;

namespace VPNBot.Handler.UpdateHandler.Message.MessageBuilder
{
    internal interface IMessageBuilder
    {
        public static abstract MessageModel Build();
    }
}
