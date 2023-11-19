namespace VpnBotApi.Worker.TelegramBot.Common
{
    public class UserOrAccessNotFountException(string message) : Exception(message)
    {
    }
}
