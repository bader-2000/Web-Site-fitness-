using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness.Models;
using Microsoft.AspNetCore.Hosting;


namespace Fitness.Controllers
{
    public class InfofitnessesController : Controller
    {
        private readonly ModelContext _context;
      
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InfofitnessesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Infofitnesses
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Infofitnesses.Include(i => i.Inprofile);
            return View(await modelContext.ToListAsync());
        }

        // GET: Infofitnesses/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Infofitnesses == null)
            {
                return NotFound();
            }

            var infofitness = await _context.Infofitnesses
                .Include(i => i.Inprofile)
                .FirstOrDefaultAsync(m => m.Idif == id);
            if (infofitness == null)
            {
                return NotFound();
            }

            return View(infofitness);
        }

        // GET: Infofitnesses/Create
        public IActionResult Create()
        {
            ViewData["Inprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid");
            return View();
        }

        // POST: Infofitnesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idif,Email,Phone,Location,Aboutus,Facebook,Linkedin,ImageFileaboutus,Inprofileid")] Infofitness infofitness)
        {
            if (ModelState.IsValid)
            {
                String wwwRootPath = _webHostEnvironment.WebRootPath;
                String filename = Guid.NewGuid().ToString() + "_" + infofitness.ImageFileaboutus.FileName;
                String path = Path.Combine(wwwRootPath + "/images/" + filename);

                using (var filestrem = new FileStream(path, FileMode.Create))
                {
                    await infofitness.ImageFileaboutus.CopyToAsync(filestrem);
                }
                infofitness.Photoaboutus = filename;
                _context.Add(infofitness);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Inprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", infofitness.Inprofileid);
            return View(infofitness);
        }








        // GET: Infofitnesses/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Infofitnesses == null)
            {
                return NotFound();
            }

            var infofitness = await _context.Infofitnesses.FindAsync(id);
            if (infofitness == null)
            {
                return NotFound();
            }

           
            return View(infofitness);
        }

        // POST: Infofitnesses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Idif,Email,Phone,Location,Aboutus,Facebook,Linkedin,ImageFileaboutus,Inprofileid")] Infofitness infofitness)
        {
            if (id != infofitness.Idif)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                 
                    if (infofitness.ImageFileaboutus != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string filename = Guid.NewGuid().ToString() + "_" + infofitness.ImageFileaboutus.FileName;
                        string path = Path.Combine(wwwRootPath + "/images/" + filename);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await infofitness.ImageFileaboutus.CopyToAsync(fileStream);
                        }

                      
                        infofitness.Photoaboutus = filename;
                    }

              
                    _context.Update(infofitness);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }

            
            ViewData["Inprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", infofitness.Inprofileid);
            return View(infofitness);
        }

        


        // GET: Infofitnesses/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Infofitnesses == null)
            {
                return NotFound();
            }

            var infofitness = await _context.Infofitnesses
                .Include(i => i.Inprofile)
                .FirstOrDefaultAsync(m => m.Idif == id);
            if (infofitness == null)
            {
                return NotFound();
            }

            return View(infofitness);
        }

        // POST: Infofitnesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Infofitnesses == null)
            {
                return Problem("Entity set 'ModelContext.Infofitnesses'  is null.");
            }
            var infofitness = await _context.Infofitnesses.FindAsync(id);
            if (infofitness != null)
            {
                _context.Infofitnesses.Remove(infofitness);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfofitnessExists(decimal id)
        {
            return _context.Infofitnesses.Any(e => e.Idif == id);
        }
    }
}
