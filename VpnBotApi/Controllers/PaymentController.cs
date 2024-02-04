using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ExtendSubscribeForBonuses = Service.ControllerService.Service.ExtendSubscribeForBonuses;
using PaymentNotification = Service.ControllerService.Service.PaymentNotification;

using GetAccessPositions = Service.ControllerService.Service.GetAccessPositions;
using GetSubscribeItem = Service.ControllerService.Service.GetSubscribeItem;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class PaymentController(ControllerServiceDispatcher dispatcher) : Controller
    {
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> AccessPositions()
        {
            var response = await dispatcher.GetService<GetAccessPositions.Result, GetAccessPositions.Request>(new GetAccessPositions.Request());

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetSubscribeItem(int id, decimal sale)
        {
            var response = await dispatcher.GetService<GetSubscribeItem.Result, GetSubscribeItem.Request>(new GetSubscribeItem.Request()
            {
                Id = id,
                Sale = sale,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> ExtendSubscribeForBonuses(ExtendSubscribeForBonuses.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
            var response = await dispatcher.GetService<bool, ExtendSubscribeForBonuses.Request>(request);

            return Json(response);
        }


        [HttpPost]
        public async Task<string> Notification([FromForm]PaymentNotification.Request request)
        {
            var response = await dispatcher.GetService<bool, PaymentNotification.Request>(request);

            if (response)
                return "YES";
            else
                return "NO";
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
