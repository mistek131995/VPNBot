using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;
using AddSubscribe = Service.ControllerService.Service.Payment.GooglePlay.AddSubscribe;
using ApplyPromoCode = Service.ControllerService.Service.Payment.ApplyPromoCode;
using CryptoCloudGetLink = Service.ControllerService.Service.Payment.CryptoCloud.GetLink;
using CryptoCloudNotification = Service.ControllerService.Service.Payment.CryptoCloud.Notification;
using ExtendSubscribeForBonuses = Service.ControllerService.Service.ExtendSubscribeForBonuses;
using GetPaymentPositions = Service.ControllerService.Service.Payment.GetPaymentPositions;
using GooglePlayNotification = Service.ControllerService.Service.Payment.GooglePlay.Notification;
using YouKassaGetLink = Service.ControllerService.Service.Payment.YouKassa.GetLink;
using YouKassaNotification = Service.ControllerService.Service.Payment.YouKassa.Notification;
using Service.ControllerService.Common;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class PaymentController(ControllerServiceDispatcher dispatcher, Serilog.ILogger logger) : Controller
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

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetCryptoCloudLink(int id, string? promoCode)
        {
            var response = await dispatcher.GetService<string, CryptoCloudGetLink.Request>(new CryptoCloudGetLink.Request()
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
            if (!IPNetwork.Parse("185.71.76.0/27").Contains(Request.HttpContext.Connection.RemoteIpAddress) &&
                !IPNetwork.Parse("185.71.77.0/27").Contains(Request.HttpContext.Connection.RemoteIpAddress) &&
                !IPNetwork.Parse("77.75.153.0/25").Contains(Request.HttpContext.Connection.RemoteIpAddress) &&
                !IPNetwork.Parse("77.75.154.128/25").Contains(Request.HttpContext.Connection.RemoteIpAddress) &&
                !IPNetwork.Parse("2a02:5180::/32").Contains(Request.HttpContext.Connection.RemoteIpAddress) &&
                IPAddress.Parse("77.75.156.11") != Request.HttpContext.Connection.RemoteIpAddress &&
                IPAddress.Parse("77.75.156.35") != Request.HttpContext.Connection.RemoteIpAddress)
            {
                logger.Information("Попытка доступа к увдомлению ЮКассы со стороннего IP адреса");
                throw new Exception("IP адреса нет в разрешенном списке");
            }

            return await dispatcher.GetService<bool, YouKassaNotification.Request>(request);
        }

        [HttpPost]
        public async Task<bool> CryptoCloudNotification()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var parsedQuery = HttpUtility.ParseQueryString(body);

            return await dispatcher.GetService<bool, CryptoCloudNotification.Request>(new CryptoCloudNotification.Request()
            {
                status = parsedQuery["status"],
                invoice_id = parsedQuery["invoice_id"],
                amount_crypto = parsedQuery["amount_crypto"],
                currency = parsedQuery["currency"],
                order_id = Guid.Parse(parsedQuery["order_id"])
            });
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
        [Authorize]
        public async Task<JsonResult> AddSubscribe([FromBody] AddSubscribe.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
            var response = await dispatcher.GetService<bool, AddSubscribe.Request>(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> GooglePlayNotification([FromBody] GooglePlayNotification.Request request)
        {
            var response = await dispatcher.GetService<bool, GooglePlayNotification.Request>(request);

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
