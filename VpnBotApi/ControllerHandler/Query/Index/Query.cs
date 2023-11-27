using VpnBotApi.Common;

namespace VpnBotApi.ControllerHandler.Query.Index
{
    public class Query : IControllerRequest<Response>
    {
        public int UserId { get; set; }
    }
}
