using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeleteLog = Service.ControllerService.Service.DeleteLog;
using GetLogs = Service.ControllerService.Service.GetLogs;
using GetVpnServers = Service.ControllerService.Service.GetServers;
using UploadFile = Service.ControllerService.Service.UploadFile;
using GetFiles = Service.ControllerService.Service.GetFiles;

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
            var response = await dispatcher.GetService<GetVpnServers.Result, GetVpnServers.Request>(new GetVpnServers.Request());

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> UploadFile([FromForm] IFormFile file, [FromForm] string version, [FromForm] string tag)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                var response = await dispatcher.GetService<bool, UploadFile.Request>(new UploadFile.Request()
                {
                    Tag = tag,
                    Name = file.FileName.Replace(" ", "_"),
                    ContentType = file.ContentType,
                    Data = ms.ToArray(),
                    Version = version
                });

                return Json(response);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetFiles()
        {
            var response = await dispatcher.GetService<GetFiles.Result, GetFiles.Request>(new GetFiles.Request());

            return Json(response);
        }
    }
}
