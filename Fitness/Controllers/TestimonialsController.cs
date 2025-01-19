using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness.Models;


namespace Fitness.Controllers
{
   
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public TestimonialsController(ModelContext context)
        {
            _context = context;
        }


        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Testimonials.Include(t => t.Tprofile);
            return View(await modelContext.ToListAsync());
        }


        // Approve
        [HttpPost]
        [ActionName("Approve")]
        public async Task<IActionResult> Approve(decimal id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(x => x.Testimoid == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.Status = "Approved";
            _context.Update(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Testimonials");
        }

        // Reject
        [HttpPost]
        [ActionName("Reject")]
        public async Task<IActionResult> Reject(decimal id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(x => x.Testimoid == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.Status = "Rejected";
            _context.Update(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Testimonials");
        }


        //// Approved 
        //// POST: Testimonials/Edit/5
        //[ValidateAntiForgeryToken]
        //[HttpPost, ActionName("Approved")]

        //public async Task<IActionResult> Approved(decimal id )
        //{

        //    var trstumo = _context.Testimonials.FirstOrDefault(x =>x.Testimoid == id );


        //    Testimonial testimonial = new Testimonial();

        //    if (id != testimonial.Testimoid)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            testimonial.Status = "Approved";
        //           _context.Update(testimonial);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TestimonialExists(testimonial.Testimoid))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index" , "Testimonials");
        //    }

        //    return View(testimonial);
        //}


        // Rejected
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[HttpPost, ActionName("Rejected")]
        //public async Task<IActionResult> Rejected(decimal id)
        //{

        //    var trstumo = _context.Testimonials.FirstOrDefault(x => x.Testimoid == id);


        //    Testimonial testimonial = new Testimonial();

        //    if (id != testimonial.Testimoid)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            testimonial.Status = "Rejected";
        //            _context.Update(testimonial);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TestimonialExists(testimonial.Testimoid))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index", "Testimonials");
        //    }

        //    return View(testimonial);
        //}






        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Tprofile)
                .FirstOrDefaultAsync(m => m.Testimoid == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testimoid,Feedback,Status,Tprofileid")] Testimonial testimonial)
        {
            
          

            if (ModelState.IsValid)
            {
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", testimonial.Tprofileid);
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", testimonial.Tprofileid);
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testimoid,Feedback,Status,Tprofileid")] Testimonial testimonial)
        {
            if (id != testimonial.Testimoid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Testimoid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", testimonial.Tprofileid);
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Tprofile)
                .FirstOrDefaultAsync(m => m.Testimoid == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Testimonials == null)
            {
                return Problem("Entity set 'ModelContext.Testimonials'  is null.");
            }
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(decimal id)
        {
          return (_context.Testimonials?.Any(e => e.Testimoid == id)).GetValueOrDefault();
        }
    }
}
