using Microsoft.AspNetCore.Mvc;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> AuthAsync()
        {
            return Json(new { });
        }
    }
}
