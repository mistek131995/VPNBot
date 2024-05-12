using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DeleteLog = Service.ControllerService.Service.DeleteLog;
using GetLogs = Service.ControllerService.Service.GetLogs;
using AddLogs = Service.ControllerService.Service.AddLog;
using GetIndexLocation = Service.ControllerService.Service.Admin.VpnLocation.GetIndex;
using GetAddEditServer = Service.ControllerService.Service.Admin.GetAddEditServer;
using UploadFile = Service.ControllerService.Service.UploadFile;
using GetFiles = Service.ControllerService.Service.GetFiles;
using GetSettings = Service.ControllerService.Service.GetSettings;
using AddCountry = Service.ControllerService.Service.AddCountry;
using AddServer = Service.ControllerService.Service.Admin.AddServer;
using TicketManagementList = Service.ControllerService.Service.Admin.TicketManagement.List;
using TicketManagementView = Service.ControllerService.Service.Admin.TicketManagement.View;
using UpdateCondition = Service.ControllerService.Service.Admin.TicketManagement.UpdateCondition;
using AddTicketMessage = Service.ControllerService.Service.Admin.TicketManagement.AddMessage;
using UpdateServer = Service.ControllerService.Service.Admin.UpdateServer;
using GetUsers = Service.ControllerService.Service.Admin.Users.GetUsers;
using GetUser = Service.ControllerService.Service.Admin.Users.GetUser;
using UpdateUser = Service.ControllerService.Service.Admin.Users.Update;
using DeleteUser = Service.ControllerService.Service.Admin.Users.Delete;
using DeleteServer = Service.ControllerService.Service.Location.DeleteServer;
using AddPromoCode = Service.ControllerService.Service.Admin.Finance.AddPromoCode;
using GetPromoCodes = Service.ControllerService.Service.Admin.Finance.GetPromoCodes;
using DeletePromoCode = Service.ControllerService.Service.Admin.Finance.DeletePromoCode;
using GetPromoCode = Service.ControllerService.Service.Admin.Finance.GetPromoCode;
using UpdatePromoCode = Service.ControllerService.Service.Admin.Finance.UpdatePromoCode;

using CreateDirectory = Service.ControllerService.Service.Admin.FileManager.CreateDirectory;
using GetDirectories = Service.ControllerService.Service.Admin.FileManager.GetDirectories;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AdminController(ControllerServiceDispatcher dispatcher, Serilog.ILogger logger) : Controller
    {
        [HttpGet]
        public async Task<JsonResult> GetSettings()
        {
            var response = await dispatcher.GetService<GetSettings.Result, GetSettings.Request>(new GetSettings.Request());
            return Json(response);
        } 

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

        [HttpPost]
        public async Task<JsonResult> AddLog(AddLogs.Request request)
        {
            var response = await dispatcher.GetService<bool, AddLogs.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetIndexLocation()
        {
            var response = await dispatcher.GetService<GetIndexLocation.Result, GetIndexLocation.Request>(new GetIndexLocation.Request());

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetAddEditServer(int id)
        {
            var response = await dispatcher.GetService<GetAddEditServer.Result, GetAddEditServer.Request>(new GetAddEditServer.Request()
            {
                ServerId = id
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> AddServer(AddServer.Request request)
        {
            var response = await dispatcher.GetService<bool, AddServer.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> AddCountry(AddCountry.Request request)
        {
            var response = await dispatcher.GetService<bool, AddCountry.Request>(request);

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetListTicketManagement()
        {
            var response = await dispatcher.GetService<TicketManagementList.Result, TicketManagementList. Request >(new TicketManagementList.Request());

            return Json(response);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetViewTicketManagement(int ticketId)
        {
            var response = await dispatcher.GetService<TicketManagementView.Result, TicketManagementView.Request>(new TicketManagementView.Request()
            {
                TicketId = ticketId
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> AddTicketMessage(AddTicketMessage.Request request)
        {
            request.UsertId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
            var response = await dispatcher.GetService<bool, AddTicketMessage.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> UpdateTicketCondition(UpdateCondition.Request request)
        {
            var response = await dispatcher.GetService<bool, UpdateCondition.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> UpdateServer(UpdateServer.Request request)
        {
            var response = await dispatcher.GetService<bool, UpdateServer.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> DeleteServer([FromBody] DeleteServer.Request request)
        {
            var response = await dispatcher.GetService<bool, DeleteServer.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetUsers()
        {
            var response = await dispatcher.GetService<GetUsers.Result, GetUsers.Request>(new GetUsers.Request());

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetUser(int Id)
        {
            var response = await dispatcher.GetService<GetUser.Result, GetUser.Request>(new GetUser.Request()
            {
                Id = Id
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> UpdateUser([FromBody]UpdateUser.Request request)
        {
            var response = await dispatcher.GetService<bool, UpdateUser.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> DeleteUser([FromBody]DeleteUser.Request request)
        {
            var response = await dispatcher.GetService<bool, DeleteUser.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> AddPromoCode([FromBody]AddPromoCode.Request request)
        {
            var response = await dispatcher.GetService<bool, AddPromoCode.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetPromoCodes()
        {
            var response = await dispatcher.GetService<GetPromoCodes.Result, GetPromoCodes.Request>(new GetPromoCodes.Request());

            return Json(response.PromoCodes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> DeletePromoCode([FromBody]DeletePromoCode.Request request)
        {
            var response = await dispatcher.GetService<bool, DeletePromoCode.Request>(request);

            return Json(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetPromoCode(int id)
        {
            var response = await dispatcher.GetService<GetPromoCode.Result, GetPromoCode.Request>(new GetPromoCode.Request()
            {
                Id = id
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> UpdatePromoCode([FromBody]UpdatePromoCode.Request request)
        {
            var response = await dispatcher.GetService<bool, UpdatePromoCode.Request>(request);

            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> CreateDirectory([FromBody] CreateDirectory.Request request)
        {
            var response = await dispatcher.GetService<bool, CreateDirectory.Request>(request);

            return Json(response);
        }
        
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetDirectories([FromBody] GetDirectories.Request request)
        {
            var response = await dispatcher.GetService<string[], GetDirectories.Request>(request);

            return Json(response);
        }
    }
}
