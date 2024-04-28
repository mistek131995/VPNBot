using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ExtendSubscribeForBonuses = Service.ControllerService.Service.ExtendSubscribeForBonuses;

using YouKassaNotification = Service.ControllerService.Service.Payment.YouKassa.Notification;
using YouKassaGetLink = Service.ControllerService.Service.Payment.YouKassa.GetLink;

using GetPaymentPositions = Service.ControllerService.Service.Payment.GetPaymentPositions;
using ApplyPromoCode = Service.ControllerService.Service.Payment.ApplyPromoCode;

using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class PaymentController(ControllerServiceDispatcher dispatcher) : Controller
    {
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetPaymentPositions()
        {
            var response = await dispatcher.GetService<GetPaymentPositions.Result, GetPaymentPositions.Request>(new GetPaymentPositions.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> ApplyPromoCode(string promoCode, int positionId)
        {
            var response = await dispatcher.GetService<int, ApplyPromoCode.Request>(new ApplyPromoCode.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value),
                PositionId = positionId,
                PromoCode = promoCode
            });

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetYouKassaLink(int id, string? promoCode)
        {
            var response = await dispatcher.GetService<string, YouKassaGetLink.Request>(new YouKassaGetLink.Request()
            {
                Id = id,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value),
                PromoCode = promoCode
            });

            return Json(response);
        }

        [HttpPost]
        public async Task<bool> YouKassaNotification([FromBody] YouKassaNotification.Request request)
        {
            Console.WriteLine(Request.HttpContext.Connection.RemoteIpAddress);

            var response = await dispatcher.GetService<bool, YouKassaNotification.Request>(request);

            return true;
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> ExtendSubscribeForBonuses(ExtendSubscribeForBonuses.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
            var response = await dispatcher.GetService<bool, ExtendSubscribeForBonuses.Request>(request);

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
