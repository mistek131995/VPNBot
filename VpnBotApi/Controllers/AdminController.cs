﻿using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;

using GetLogs = Service.ControllerService.Service.GetLogs;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AdminController(ControllerServiceDispatcher dispatcher) : Controller
    {
        public async Task<JsonResult> GetLogs()
        {
            var response = await dispatcher.GetService<GetLogs.Result, GetLogs.Request>(new GetLogs.Request());
            return Json(response);
        }
    }
}
