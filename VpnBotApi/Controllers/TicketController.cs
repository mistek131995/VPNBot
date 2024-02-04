using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AddTicket = Service.ControllerService.Service.AddTicket;
using GetAddTicketForm = Service.ControllerService.Service.GetAddTicketForm;


namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class TicketController(ControllerServiceDispatcher dispatcher) : Controller
    {

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetAddTicketForm()
        {
            var response = await dispatcher.GetService<GetAddTicketForm.Result, GetAddTicketForm.Request>(new GetAddTicketForm.Request());

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> CreateTicket([FromBody] AddTicket.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);

            var response = await dispatcher.GetService<int, AddTicket.Request>(request);

            return Json(response);
        }
    }
}
