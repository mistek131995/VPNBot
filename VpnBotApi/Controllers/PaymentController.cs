using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ExtendSubscribeForBonuses = Service.ControllerService.Service.ExtendSubscribeForBonuses;

//using YouKassaNotification = Service.ControllerService.Service.Payment.YouKassa.Notification;
using YouKassaGetLink = Service.ControllerService.Service.Payment.YouKassa.GetLink;

using CreateRuKassaLink = Service.ControllerService.Service.Payment.RuKassa.CreateLink;
using RuKassaNotification = Service.ControllerService.Service.Payment.RuKassa.Notification;

using GetPaymentPositions = Service.ControllerService.Service.Payment.GetPaymentPositions;
using ApplyPromoCode = Service.ControllerService.Service.Payment.ApplyPromoCode;

using CreatePayOkLink = Service.ControllerService.Service.Payment.PayOk.CreateLink;
using PayOkNotification = Service.ControllerService.Service.Payment.PayOk.Notification;
using Newtonsoft.Json;

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
        [Authorize]
        public async Task<JsonResult> ExtendSubscribeForBonuses(ExtendSubscribeForBonuses.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
            var response = await dispatcher.GetService<bool, ExtendSubscribeForBonuses.Request>(request);

            return Json(response);
        }

        //[HttpPost]
        //public async Task<string> Notification([FromForm] FreeKassaNotification.Request request)
        //{
        //    var response = await dispatcher.GetService<bool, FreeKassaNotification.Request>(request);

        //    if (response)
        //        return "YES";
        //    else
        //        return "NO";
        //}

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> CreateRuKassaLink(int id, string? promocode)
        {
            var response = await dispatcher.GetService<string, CreateRuKassaLink.Request>(new CreateRuKassaLink.Request()
            {
                Id = id,
                PromoCode = promocode,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> CreatePayOkLink(int id, string? promocode)
        {
            var response = await dispatcher.GetService<string, CreatePayOkLink.Request>(new CreatePayOkLink.Request()
            {
                Id = id,
                PromoCode = promocode,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> PayOkNotification([FromForm] PayOkNotification.Request request)
        {
            var response = await dispatcher.GetService<bool, PayOkNotification.Request>(request);

            return Json(response);
        }

        public async Task RuKassaNotification()
        {
            var signature = Request.Headers.FirstOrDefault(x => x.Key == "signature").Value;

            await dispatcher.GetService<bool, RuKassaNotification.Request>(new RuKassaNotification.Request()
            {
                Query = await new StreamReader(Request.Body).ReadToEndAsync(),
                Signature = signature
            });
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
