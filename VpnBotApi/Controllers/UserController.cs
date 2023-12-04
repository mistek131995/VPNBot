﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using GetLinkAuth = Service.ControllerService.Service.GetLinkAuth;
using GetIndex = Service.ControllerService.Service.GetIndex;

using LoginByLink = Service.ControllerService.Service.AuthByLink;
using SetLoginAndPassword = Service.ControllerService.Service.SetLoginAndPassword;
using ChangePassword = Service.ControllerService.Service.ChangePassword;
using Application.ControllerService.Common;


namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController(ControllerServiceDispatcher dispatcher) : Controller
    {

        [HttpGet]
        public async Task<JsonResult> LoginByLink([FromQuery] GetLinkAuth.Request query)
        {
            var response = await dispatcher.GetService<GetLinkAuth.Result, GetLinkAuth.Request>(query);

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
        public async Task<JsonResult> LoginByLink([FromBody] LoginByLink.Request command)
        {
            var response = await dispatcher.GetService<string, LoginByLink.Request>(command);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> SetLoginAndPassword([FromBody] SetLoginAndPassword.Request query)
        {
            var response = await dispatcher.GetService<bool, SetLoginAndPassword.Request>(query);

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ChangePassword([FromBody] ChangePassword.Request command)
        {
            command.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<bool, ChangePassword.Request>(command);

            return Json(response);
        }
    }
}
