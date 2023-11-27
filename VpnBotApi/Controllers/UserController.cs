using Microsoft.AspNetCore.Mvc;
using VpnBotApi.Common;
using LoginByLinkQuery = VpnBotApi.ControllerHandler.Query.LinkAuth;
using IndexQuery = VpnBotApi.ControllerHandler.Query.Index;
using LoginByLinkCommand = VpnBotApi.ControllerHandler.Command.LinkAuth;
using SetLoginAndPasswordCommand = VpnBotApi.ControllerHandler.Command.SetLoginAndPassword;
using Microsoft.AspNetCore.Authorization;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController(ControllerDispatcher dispatcher) : Controller
    {

        [HttpGet]
        public async Task<JsonResult> LoginByLink([FromQuery] LoginByLinkQuery.Query query)
        {
            var response = await dispatcher.BuildHandler<LoginByLinkQuery.Response, LoginByLinkQuery.Query>(query);

            return Json(response);
        }

        [HttpGet]
        //[Authorize]
        public async Task<JsonResult> GetIndex()
        {
            var query = new IndexQuery.Query()
            {

                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            };

            var response = await dispatcher.BuildHandler<IndexQuery.Response, IndexQuery.Query>(query);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> LoginByLink([FromBody]LoginByLinkCommand.Command command)
        {
            var response = await dispatcher.BuildHandler<string, LoginByLinkCommand.Command>(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> SetLoginAndPassword([FromBody] SetLoginAndPasswordCommand.Command query)
        {
            var response = await dispatcher.BuildHandler<bool, SetLoginAndPasswordCommand.Command>(query);

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
