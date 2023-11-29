namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQueryHandler.BuyAccess
{
    public class Response
    {
        public string Text { get; set; }
        public byte[] AccessQrCode { get; set; }
    }
}
