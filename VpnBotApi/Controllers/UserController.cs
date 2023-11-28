using Microsoft.AspNetCore.Mvc;
using VpnBotApi.Common;
using Microsoft.AspNetCore.Authorization;

using LoginByLinkQuery = VpnBotApi.ControllerHandler.Query.LinkAuth;
using IndexQuery = VpnBotApi.ControllerHandler.Query.Index;

using LoginByLinkCommand = VpnBotApi.ControllerHandler.Command.LinkAuth;
using SetLoginAndPasswordCommand = VpnBotApi.ControllerHandler.Command.SetLoginAndPassword;
using ChangePassword = VpnBotApi.ControllerHandler.Command.ChangePassword;


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
        [Authorize]
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

        [HttpPost]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePassword.Command command)
        {
            command.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.BuildHandler<bool, ChangePassword.Command>(command);

            return Json(response);
        }
    }
}
