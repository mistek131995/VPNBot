using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;

using GetFile = Service.ControllerService.Service.GetFile;
using GetCountries = Service.ControllerService.Service.GetVpnCountries;

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
        public async Task<JsonResult> GetCountries()
        {
            var response = await dispatcher.GetService<GetCountries.Result, GetCountries.Request>(new GetCountries.Request());

            return Json(response.Countries);
        }
    }
}
