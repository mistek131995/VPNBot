using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.ControllerService.Common;

using GetIndex = Service.ControllerService.Service.GetIndex;

using Register = Service.ControllerService.Service.User.Register;
using Activation = Service.ControllerService.Service.ActivateUser;
using Login = Service.ControllerService.Service.User.AuthByLogin;
using ChangePassword = Service.ControllerService.Service.User.ChangePassword;
using CreateResetPasswordLink = Service.ControllerService.Service.User.RestorePassword.CreateResetPasswordLink;
using SetNewPassword = Service.ControllerService.Service.User.RestorePassword.SetNewPassword;

using ReferralIndex = Service.ControllerService.Service.ReferralIndex;


namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController(ControllerServiceDispatcher dispatcher) : Controller
    {

        [HttpGet]
        public async Task<JsonResult> Activation(Guid guid)
        {
            var response = await dispatcher.GetService<bool, Activation.Request>(new Activation.Request()
            {
                Guid = guid
            });

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> Register([FromBody] Register.Request request)
        {
            var response = await dispatcher.GetService<bool, Register.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetIndex()
        {
            var query = new GetIndex.Request()
            {

                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            };

            var response = await dispatcher.GetService<GetIndex.Result, GetIndex.Request>(query);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody] Login.Request request)
        {
            var test = HttpContext.Connection.RemoteIpAddress;

            var response = await dispatcher.GetService<string, Login.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePassword.Request command)
        {
            command.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<bool, ChangePassword.Request>(command);

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetReferralIndex()
        {
            var response = await dispatcher.GetService<ReferralIndex.Result, ReferralIndex.Request>(new ReferralIndex.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> CreateResetPasswordLink([FromBody] CreateResetPasswordLink.Request request)
        {
            var response = await dispatcher.GetService<bool, CreateResetPasswordLink.Request>(request);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> SetNewPassword([FromBody] SetNewPassword.Request request)
        {
            var response = await dispatcher.GetService<bool, SetNewPassword.Request>(request);

            return Json(response);
        }
    }
}
