using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using GetIndex = Service.ControllerService.Service.GetIndex;
using GetAccessPositions = Service.ControllerService.Service.GetAccessPositions;

using Register = Service.ControllerService.Service.Register;
using Activation = Service.ControllerService.Service.ActivateUser;
using Login = Service.ControllerService.Service.AuthByLogin;
using ChangePassword = Service.ControllerService.Service.ChangePassword;
using Application.ControllerService.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


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
        public async Task<JsonResult> Register(Register.Request request)
        {
            var response = await dispatcher.GetService<bool, Register.Request>(request);

            return Json(response);
        }

        [HttpGet]
        public async Task<string> GetAccess()
        {
            var access = "vless://4a2cc04a-2184-4311-be9c-15216ac09461@2.59.183.140:443?type=tcp&security=reality&fp=chrome&pbk=K3aNlmMC_2WU39eLkUGcp4arcNpc8ze1aKTnpcS-tAc&sni=yahoo.com&sid=ab2cc97b&spx=%2F#subscribe-access-vpn2";

            return access;
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
        public async Task<JsonResult> AccessPositions()
        {
            var response = await dispatcher.GetService< GetAccessPositions.Result, GetAccessPositions.Request>(new GetAccessPositions.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }
    }
}
