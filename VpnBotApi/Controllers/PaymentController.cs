using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;

using PaymentNotification = Service.ControllerService.Service.PaymentNotification;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class PaymentController(ControllerServiceDispatcher dispatcher) : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Notification([FromForm]PaymentNotification.Request request)
        {
            var response = await dispatcher.GetService<bool, PaymentNotification.Request>(request);

            return Json(response);
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
