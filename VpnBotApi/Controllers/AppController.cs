using Microsoft.AspNetCore.Mvc;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AppController : Controller
    {

        [HttpGet]
        public async Task<JsonResult> AppVersion()
        {
            return Json("1.0.0");
        }

        [HttpGet]
        public async Task<JsonResult> CheckCoreHash(string hash)
        {


            return Json(new { });
        }
    }
}
