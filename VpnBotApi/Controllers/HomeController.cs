using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VpnBotApi.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        private readonly Context context;
        public HomeController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<JsonResult> Index()
        {
            var users = await context.Users.ToListAsync();
            return Json(users);
        }
    }
}
