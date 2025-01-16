using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger ,ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
			ViewBag.UserIsEnter = null;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            
            var infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();

           
            return View(infowebsite);
        }

        public IActionResult aboutus()
        {
            return View();
        }
		public IActionResult trainers()
		{
			return View();
		}
		public IActionResult Class()
		{
			return View();
		}
		public IActionResult blog()
		{
			return View();
		}
		public IActionResult contactus()
		{
			return View();
		}

        public IActionResult payment()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
