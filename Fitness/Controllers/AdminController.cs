﻿using Fitness.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging.Core;
using System.Linq;
using System.Collections.Generic;


namespace Fitness.Controllers
{
    

    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(decimal? id)
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



            if(id != null)
            return RedirectToAction(id == 2 && id != 1 ? "listProfileT" : "listProfileM");
            return View();
        }


        //Get profile tranier 

        public async Task<IActionResult> listProfileT()
        {
            var modelContext = _context.Profiles
                .Include(p => p.Role)  
                .Where(p => p.Role.Roleid == 2);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> listProfileM()
        {
            var modelContext = _context.Profiles
                .Include(p => p.Role)  
                .Where(p => p.Role.Roleid == 3);
            return View(await modelContext.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Role)  
                .FirstOrDefaultAsync(m => m.Profileid == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            ViewBag.Roleid = new SelectList(_context.Roles.Where(r => r.Roleid == 2 || r.Roleid == 3),  "Roleid", "Rname");
            return View();
        }



        // post  Search 
        public async Task<IActionResult> Search()
        {
            
            var filteredData = await _context.Typepeople
                .Include(t => t.Tsubscr)  
                .ToListAsync(); 

          
            return View(filteredData);
        }

        // post start date 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(DateTime? StartDate, DateTime? EndDate)
        {
           
            if (StartDate == null && EndDate == null)
            {
                var data = await _context.Typepeople
                    .Include(t => t.Tsubscr) 
                    .ToListAsync(); 
                return View(data);
            }

           
            if (StartDate == null)
            {
                var data = await _context.Typepeople
                    .Include(t => t.Tsubscr)
                    .Where(d => d.Enddate <= EndDate) 
                    .ToListAsync();
                return View(data);
            }

            
            if (EndDate == null)
            {
                var data = await _context.Typepeople
                    .Include(t => t.Tsubscr) 
                    .Where(d => d.Startdate >= StartDate)
                    .ToListAsync();
                return View(data);
            }

           
            var filteredData = await _context.Typepeople
                .Include(t => t.Tsubscr)  
                .Where(d => d.Startdate >= StartDate && d.Enddate <= EndDate)
                .ToListAsync();

            return View(filteredData);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Profileid,Name,Username,Userpassword,ImageFile,DateOfBirth,Email,Lname,Roleid")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                if (profile.ImageFile != null)
                {
                    String wwwRootPath = _webHostEnvironment.WebRootPath;
                    String filename = Guid.NewGuid().ToString() + "_" + profile.ImageFile.FileName;
                    String path = Path.Combine(wwwRootPath + "/images/" + filename);

                    using (var filestrem = new FileStream(path, FileMode.CreateNew))
                    {
                        await profile.ImageFile.CopyToAsync(filestrem);
                    }
                    profile.Photo = filename;
                }

                _context.Add(profile);
                await _context.SaveChangesAsync();
                return (profile.Roleid == 2) ? RedirectToAction("listProfileT") : RedirectToAction("listProfileM");
            }

            ViewBag.Roleid = new SelectList(_context.Roles.Where(r => r.Roleid == 2 || r.Roleid == 3), "Roleid", "Rname", profile.Roleid);
            return  (profile.Roleid == 2) ? RedirectToAction("listProfileT") : RedirectToAction("listProfileM"); ;
        }



        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }


            ViewBag.Roleid = new SelectList(_context.Roles.Where(r => r.Roleid == 2 || r.Roleid == 3), "Roleid", "Rname");
            return View(profile);
        }


        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Profileid,Name,Username,Userpassword,ImageFile,DateOfBirth,Email,Lname,Roleid")] Profile profile)
        {
            if (id != profile.Profileid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (profile.ImageFile != null)
                    {
                        String wwwRootPath = _webHostEnvironment.WebRootPath;
                        String filename = Guid.NewGuid().ToString() + "_" + profile.ImageFile.FileName;
                        String path = Path.Combine(wwwRootPath + "/images/" + filename);

                        using (var filestrem = new FileStream(path, FileMode.CreateNew))
                        {
                            await profile.ImageFile.CopyToAsync(filestrem);
                        }
                        profile.Photo = filename;
                    }

                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Profileid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return  (profile.Roleid == 2) ? RedirectToAction("listProfileT") : RedirectToAction("listProfileM");
            }
           
           
            ViewBag.Roleid = new SelectList(_context.Roles.Where(r => r.Roleid == 2 || r.Roleid == 3), "Roleid", "Rname");
            return View(profile);
        }

        // GET: Profiles/Delete/5

        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.Profileid == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }





        [HttpPost]
        public IActionResult Report(DateTime? StartDate, DateTime? EndDate, string subscriptionType)
        {
            var subscriptions = _context.Subscriptions.Include(s => s.Typepeople).ToList();
            var multiTable = from s in subscriptions
                             from tp in s.Typepeople
                             select new
                             {
                                 Subscription = s,
                                 Typeperson = tp
                             };

            var filteredData = multiTable.AsQueryable();

            if (StartDate != null && EndDate != null)
            {
                filteredData = filteredData.Where(x => x.Typeperson.Startdate >= StartDate && x.Typeperson.Enddate <= EndDate);
            }
            else if (StartDate != null)
            {
                filteredData = filteredData.Where(x => x.Typeperson.Startdate >= StartDate);
            }
            else if (EndDate != null)
            {
                filteredData = filteredData.Where(x => x.Typeperson.Enddate <= EndDate);
            }

            if (!string.IsNullOrEmpty(subscriptionType))
            {
                filteredData = filteredData.Where(x => x.Subscription.Nameplan == subscriptionType);
            }

            ViewBag.TotalQuantity = filteredData.Sum(x => x.Subscription.Countweeks ?? 0);
            ViewBag.TotalPrice = filteredData.Sum(x => x.Subscription.Price ?? 0);

            return View(filteredData.ToList());
        }

        [HttpGet]
        public IActionResult Report()
        {
            
            var subscriptions = _context.Subscriptions.Include(s => s.Typepeople).ToList();
            var typepeople = _context.Typepeople.Include(t => t.Tsubscr).Include(t => t.Tprofile).ToList();

            var multiTable = from s in subscriptions
                             from tp in s.Typepeople 
                             select new
                             {
                                 Subscription = s,
                                 Typeperson = tp
                             };

            return View(multiTable.ToList());
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'ModelContext.Profiles'  is null.");
            }
            var profile = await _context.Profiles.FindAsync(id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
            }
            await _context.SaveChangesAsync();
            return  ( id == 2) ? RedirectToAction("listProfileT") : RedirectToAction("listProfileM"); ;
        }

        //get Data For chart 

        public IActionResult OnGet()
        {
            
            var chartData = _context.Typepeople
                .Where(tp => tp.Startdate.HasValue) 
                .GroupBy(tp => tp.Startdate.Value.Month) 
                .Select(g => new
                {
                    Month = g.Key,
                    Subscriptions = g.Count() 
                })
                .OrderBy(d => d.Month)
                .ToList();

            
            ViewData["ChartData"] = JsonConvert.SerializeObject(chartData);

            return View();
        }





        private bool ProfileExists(decimal id)
        {
            return (_context.Profiles?.Any(e => e.Profileid == id)).GetValueOrDefault();
        }
    }
}