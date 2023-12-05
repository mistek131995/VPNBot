using Microsoft.AspNetCore.Mvc;

namespace VpnBotApi.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
