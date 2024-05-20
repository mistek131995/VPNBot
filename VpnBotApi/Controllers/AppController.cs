﻿using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;

using GetFile = Service.ControllerService.Service.GetFile;
using GetVpnLocation = Service.ControllerService.Service.App.GetVpnLocation;
using GetServersByTag = Service.ControllerService.Service.App.GetServersByTag;
using GetVpnConnectionByIp = Service.ControllerService.Service.App.GetConnectionByIP;
using GetProxyConnection = Service.ControllerService.Service.App.GetProxyConnection;

using GetConnectionScreen = Service.ControllerService.Service.App.GetConnectionScreen;
using AddError = Service.ControllerService.Service.App.AddError;

using GetSubscribeModal = Service.ControllerService.Service.App.GetSubscribeModal;

using Microsoft.AspNetCore.Authorization;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AppController(ControllerServiceDispatcher dispatcher) : Controller
    {
        [HttpGet]
        public async Task<JsonResult> GetFile(string tag)
        {
            var response = await dispatcher.GetService<GetFile.Result, GetFile.Request>(new GetFile.Request()
            {
                Tag = tag
            });

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetLocations()
        {
            var response = await dispatcher.GetService<GetVpnLocation.Result, GetVpnLocation.Request>(new GetVpnLocation.Request());

            return Json(response.Locations);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetServersByTag(string tag)
        {
            var response = await dispatcher.GetService<GetServersByTag.Result, GetServersByTag.Request>(new GetServersByTag.Request()
            {
                Tag = tag
            });

            return Json(response.Servers);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetConnectionByIp(string ip)
        {
            var response = await dispatcher.GetService<GetVpnConnectionByIp.Result, GetVpnConnectionByIp.Request>(new GetVpnConnectionByIp.Request()
            {
                Ip = ip,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }


        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetConnectionScreen()
        {
            var response = await dispatcher.GetService<GetConnectionScreen.Result, GetConnectionScreen.Request>(new GetConnectionScreen.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddError([FromBody]AddError.Request request)
        {
            var response = await dispatcher.GetService<bool, AddError.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetProxyConnection(string ip)
        {
            var response = await dispatcher.GetService<GetProxyConnection.Result, GetProxyConnection.Request>(new GetProxyConnection.Request()
            {
                Ip = ip,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetSubscribeModal()
        {
            var response = await dispatcher.GetService<GetSubscribeModal.Result, GetSubscribeModal.Request>(new GetSubscribeModal.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }
    }
}
