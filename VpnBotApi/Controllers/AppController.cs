using Application.ControllerService.Common;
using Microsoft.AspNetCore.Mvc;

using GetFile = Service.ControllerService.Service.GetFile;
using GetCountries = Service.ControllerService.Service.GetVpnCountries;
using GetVpnConnection = Service.ControllerService.Service.GetConnection;
using GetXRayConfig = Service.ControllerService.Service.GetXRayConfig;
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
        public async Task<JsonResult> GetCountries()
        {
            var response = await dispatcher.GetService<GetCountries.Result, GetCountries.Request>(new GetCountries.Request());

            return Json(response.Countries);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetConnection(int countryId)
        {
            var response = await dispatcher.GetService<GetVpnConnection.Result, GetVpnConnection.Request>(new GetVpnConnection.Request()
            {
                CountryId = countryId
            });

            return Json(response);
        }

        [HttpGet]
        public async Task<JsonResult> GetXRayConfig(int countryId)
        {
            var response = await dispatcher.GetService<GetXRayConfig.Result, GetXRayConfig.Request>(new GetXRayConfig.Request()
            {
                CountryId = countryId
            });

            return Json(response);
        }
    }
}
