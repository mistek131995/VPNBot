using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;
using Update = Service.ControllerService.Service.UpdateByTag;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AppController(ControllerServiceDispatcher dispatcher) : Controller
    {
        [HttpGet]
        public async Task<JsonResult> UpdateByTag(string tag)
        {
            var response = await dispatcher.GetService<Update.Result, Update.Request>(new Update.Request()
            {
                Tag = tag
            });

            return Json(response);
        }
    }
}
