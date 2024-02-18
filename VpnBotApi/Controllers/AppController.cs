﻿using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;

using GetFile = Service.ControllerService.Service.GetFile;
using GetVpnLocation = Service.ControllerService.Service.App.GetVpnLocation;
using GetServers = Service.ControllerService.Service.App.GetServers;
using GetServersByTag = Service.ControllerService.Service.App.GetServersByTag;
using GetVpnConnectionByIp = Service.ControllerService.Service.App.GetConnectionByIP;

using GetVpnConnection = Service.ControllerService.Service.GetConnection;
using GetConnectionScreen = Service.ControllerService.Service.GetConnectionScreen;
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
        [Authorize]//Устарел
        public async Task<JsonResult> GetCountries()
        {
            var response = await dispatcher.GetService<GetVpnLocation.Result, GetVpnLocation.Request>(new GetVpnLocation.Request());

            return Json(response.Countries);
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
        public async Task<JsonResult> GetServers()
        {
            var response = await dispatcher.GetService<GetServers.Result, GetServers.Request>(new GetServers.Request());

            return Json(response.Servers);
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
        [Authorize]//Устарел
        public async Task<JsonResult> GetConnection(int countryId)
        {
            var response = await dispatcher.GetService<GetVpnConnection.Result, GetVpnConnection.Request>(new GetVpnConnection.Request()
            {
                CountryId = countryId,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
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
    }
}
