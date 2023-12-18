using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GetLogs = Service.ControllerService.Service.GetLogs;
using GetVpnServers = Service.ControllerService.Service.GetServers;
using DeleteLog = Service.ControllerService.Service.DeleteLog;

using UploadFile = Service.ControllerService.Service.UploadFile;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AdminController(ControllerServiceDispatcher dispatcher) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetLogs()
        {
            var response = await dispatcher.GetService<GetLogs.Result, GetLogs.Request>(new GetLogs.Request());
            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> DeleteLog(DeleteLog.Request request)
        {
            var response = await dispatcher.GetService<bool, DeleteLog.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetServers()
        {
            var response = await dispatcher.GetService< GetVpnServers.Result, GetVpnServers.Request>(new GetVpnServers.Request());

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var response = await dispatcher.GetService<bool, UploadFile.Request>(new UploadFile.Request()
                {
                    Tag = file.FileName.Trim().Replace(" ", "_"),
                    Name = file.FileName,
                    ContentType = file.ContentType,
                    Data = ms.ToArray()
                });

                return Json(response);
            }
        }
    }
}
