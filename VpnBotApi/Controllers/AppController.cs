using Microsoft.AspNetCore.Mvc;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AppController : Controller
    {
        public async Task<string> AppVersion()
        {
            return "1.0.0";
        } 
    }
}
