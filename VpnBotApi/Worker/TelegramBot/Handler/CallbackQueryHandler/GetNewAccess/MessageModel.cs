namespace VpnBotApi.Worker.TelegramBot.Handler.CallbackQuery.GetNewAccess
{
    public class MessageModel
    {
        public string Text { get; set; }
        public byte[] AccessQrCode { get; set; }

        public MessageModel(string text, byte[] accessQrCode)
        {
            Text = text;
            AccessQrCode = accessQrCode;
        }
    }
}
