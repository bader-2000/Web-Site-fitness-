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
        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            ViewBag.UserIsEnter = null;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index()
        {

            var infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();

            var profileList = await _context.Profiles.OrderBy(x => Guid.NewGuid()).Take(6).ToListAsync();

			

			if (infowebsite == null)
			{

				 infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();
			}
			else
			{
				ViewBag.infowebsite = infowebsite;
				ViewBag.infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();
				ViewBag.Profile = profileList.Take(9).ToList();
			}
			

			return View();
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



        // get testamonial
        public async Task<IActionResult> Create()
        {

			var infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();

			var profileList = await _context.Profiles.OrderBy(x => Guid.NewGuid()).Take(6).ToListAsync();



			if (infowebsite == null)
			{

				infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();
			}
			else
			{
				ViewBag.infowebsite = infowebsite;
				ViewBag.infowebsite = await _context.Infofitnesses.FirstOrDefaultAsync();
				ViewBag.Profile = profileList.Take(9).ToList();
			}

			var UserID = HttpContext.Session.GetInt32("UserID");
            var UserIsEnter = HttpContext.Session.GetInt32("UserIsEnter");
            if (UserIsEnter == null)
            { UserIsEnter = 0; }

            if (UserIsEnter == 0)
            {
                return RedirectToAction("loginAndRegister", "Auth");
            }





            return View();



        }
        // create testamonial
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Testimoid,Feedback,Status,Tprofileid")] Testimonial testimonial)
		{

             

			if (ModelState.IsValid)
			{

				testimonial.Status = "Pending";

                _context.Add(testimonial);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", testimonial.Tprofileid);
			return View(testimonial);
		}




		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
