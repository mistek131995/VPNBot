using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.ControllerService.Common;
using CreateTicket = Service.ControllerService.Service.CreateTicket;

using GetIndex = Service.ControllerService.Service.GetIndex;
using GetAccessPositions = Service.ControllerService.Service.GetAccessPositions;

using Register = Service.ControllerService.Service.Register;
using Activation = Service.ControllerService.Service.ActivateUser;
using Login = Service.ControllerService.Service.AuthByLogin;
using ChangePassword = Service.ControllerService.Service.ChangePassword;


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
        public async Task<JsonResult> AccessPositions()
        {
            var response = await dispatcher.GetService< GetAccessPositions.Result, GetAccessPositions.Request>(new GetAccessPositions.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CreateTicket([FromBody] CreateTicket.Request request)
        {
            var response = await dispatcher.GetService<int, CreateTicket.Request>(new CreateTicket.Request());

            return Json(response);
        }
    }
}
