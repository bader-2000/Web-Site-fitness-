using Microsoft.AspNetCore.Mvc;

namespace Fitness.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
