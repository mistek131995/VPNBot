using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ExtendSubscribeForBonuses = Service.ControllerService.Service.ExtendSubscribeForBonuses;
using PaymentNotification = Service.ControllerService.Service.PaymentNotification;

using CreateRuKassaLink = Service.ControllerService.Service.Payment.RuKassa.CreateLink;
using CreateLavaLink = Service.ControllerService.Service.Payment.Lava.CreateLink;
using LavaNotification = Service.ControllerService.Service.Payment.Lava.Notification;
using RuKassaNotification = Service.ControllerService.Service.Payment.RuKassa.Notification;

using GetPaymentPositions = Service.ControllerService.Service.Payment.GetPaymentPositions;
using GetSubscribeItem = Service.ControllerService.Service.GetSubscribeItem;

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
            var response = await dispatcher.GetService<GetPaymentPositions.Result, GetPaymentPositions.Request>(new GetPaymentPositions.Request());

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
        [Authorize]
        public async Task<JsonResult> CreateLavaLink(int id)
        {
            var response = await dispatcher.GetService<string, CreateLavaLink.Request>(new CreateLavaLink.Request()
            {
                Id = id,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> CreateRuKassaLink(int id)
        {
            var response = await dispatcher.GetService<string, CreateRuKassaLink.Request>(new CreateRuKassaLink.Request()
            {
                Id = id,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        public async Task LavaNotification([FromBody] LavaNotification.Request request)
        {
            request.Signature = Request.Headers.Authorization.ToString();

            Console.WriteLine("Начало выполнения------------------------------------");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            //Console.WriteLine("Заголовок------------------------------------");
            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Request.Headers));
            //Console.WriteLine("Подпись------------------------------------");
            //Console.WriteLine(Request.Headers.Authorization.ToString());
            //Console.WriteLine("Тело строкой------------------------------------");
            //Console.WriteLine(Request.Body.ToString());
            Console.WriteLine("Конец выполнения------------------------------------");

            await dispatcher.GetService<bool, LavaNotification.Request>(request);
        }

        public async Task RuKassaNotification([FromBody] RuKassaNotification.Request request)
        {
            request.Signature = Request.Headers.FirstOrDefault(x => x.Key == "signature").Value;
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            Console.WriteLine("---------------------------------------------");

            await dispatcher.GetService<bool, RuKassaNotification.Request>(request);
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
