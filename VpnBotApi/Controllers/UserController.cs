using Microsoft.AspNetCore.Mvc;
using VpnBotApi.Common;
using Auth = VpnBotApi.ControllerHandler.Auth;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController(ControllerDispatcher dispatcher) : Controller
    {

        [HttpGet]
        public async Task<JsonResult> LoginByLink([FromQuery] Auth.Query query)
        {
            var response = await dispatcher.BuildHandler<Auth.Response, Auth.Query>(query);

            return Json(response);
        }

        //[HttpGet]
        //public IActionResult Login([FromQuery] Auth.Query query)
        //{
        //    var telegramBotToken = configuration["TelegramBotToken"];

        //    var loginWidget = new LoginWidget(telegramBotToken);

        //    var auth = loginWidget.CheckAuthorization(new SortedDictionary<string, string>()
        //        {
        //            {"id", query.id},
        //            {"first_name", query.first_name},
        //            {"username", query.username},
        //            {"photo_url", query.photo_url},
        //            {"auth_date", query.auth_date},
        //            {"hash", query.hash},
        //            {"last_name", query.last_name }
        //        });

        //    return Unauthorized();
        //}


    }
}
