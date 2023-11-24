namespace VpnBotApi.ControllerHandler.LinkAuth
{
    public class Response
    {
        public UserState State { get; set; }

        public enum UserState
        {
            NotFound = 0,
            NeedSetPassword = 1,
            Ready = 2,
        }
    }
}
