using Application.ControllerService.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AddTicket = Service.ControllerService.Service.AddTicket;
using GetAddTicketForm = Service.ControllerService.Service.GetAddTicketForm;
using GetUserTickets = Service.ControllerService.Service.Ticket.GetUserTickets;
using GetTicket = Service.ControllerService.Service.Ticket.GetTicket;
using AddMessage = Service.ControllerService.Service.Ticket.AddMessage;


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

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUserTickets()
        {
            var response = await dispatcher.GetService<GetUserTickets.Result, GetUserTickets.Request>(new GetUserTickets.Request()
            {
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetTicket(int ticketId)
        {
            var response = await dispatcher.GetService<GetTicket.Result, GetTicket.Request>(new GetTicket.Request()
            {
                TicketId = ticketId,
                UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value)
            });

            return Json(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> AddMessage([FromForm] AddMessage.Request request)
        {
            request.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
            request.FormFiles = HttpContext.Request.Form.Files;

            var response = await dispatcher.GetService<bool, AddMessage.Request>(request);

            return Json(response);
        }
    }
}
