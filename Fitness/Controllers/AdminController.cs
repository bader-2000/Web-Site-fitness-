using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fitness.Controllers
{
    

    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        public AdminController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var lastRegesterd= _context.Profiles.Max(x => x.Profileid);

            ViewBag.numberofRegistered = _context.Profiles.Count();
            ViewBag.lastRegesterd = _context.Profiles
                                    .Where(x => x.Profileid > lastRegesterd) 
                                     .Sum(x => x.Profileid);
          
            ViewBag.numberofSubscreption = _context.Typepeople.Count(x => x.Status == "Active");
            ViewBag.numberOfAllSubscreption = _context.Typepeople.Count();
            ViewBag.CountofPlan = _context.Workoutplans.Count();
            ViewBag.TotalRevenue = _context.Subscriptions.Sum(total => total.Price);

            return View();
        }

        public IActionResult traner()
        {
            var lastRegesterd = _context.Profiles.Max(x => x.Profileid);

            ViewBag.numberofRegistered = _context.Profiles.Count();
            ViewBag.lastRegesterd = _context.Profiles
                                    .Where(x => x.Profileid > lastRegesterd)
                                     .Sum(x => x.Profileid);

            ViewBag.numberofSubscreption = _context.Typepeople.Count(x => x.Status == "Active");
            ViewBag.numberOfAllSubscreption = _context.Typepeople.Count();
            ViewBag.CountofPlan = _context.Workoutplans.Count();
            ViewBag.TotalRevenue = _context.Subscriptions.Sum(total => total.Price);

            return View();
        }

        //public IActionResult ProfileTranier ()
        //{
        //    return _context.Profiles != null ?
        //    View(await _context.Profiles.ToListAsync()) :
        //    Problem("Entity set 'ModelContext.Profiles'  is null.");
        //    return View();
        //}
    }
}