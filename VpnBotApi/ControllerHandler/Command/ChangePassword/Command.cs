using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Command.ChangePassword
{
    public class Command : IControllerRequest<bool>
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }
}
