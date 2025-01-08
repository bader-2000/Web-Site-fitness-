﻿using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fitness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
