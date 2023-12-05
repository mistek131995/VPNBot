using Microsoft.AspNetCore.Mvc;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class PaymentController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Notification()
        {
            return Json(new { });
        }

        [HttpGet]
        public async Task<JsonResult> Success()
        {
            return Json(new { });
        }

        [HttpGet]
        public async Task<JsonResult> Failure()
        {
            return Json(new { });
        }
    }
}
