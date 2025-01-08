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
    public class TypepersonsController : Controller
    {
        private readonly ModelContext _context;

        public TypepersonsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Typepersons
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Typepeople.Include(t => t.Tprofile).Include(t => t.Tsubscr);
            return View(await modelContext.ToListAsync());
        }

        // GET: Typepersons/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Typepeople == null)
            {
                return NotFound();
            }

            var typeperson = await _context.Typepeople
                .Include(t => t.Tprofile)
                .Include(t => t.Tsubscr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeperson == null)
            {
                return NotFound();
            }

            return View(typeperson);
        }

        // GET: Typepersons/Create
        public IActionResult Create()
        {
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid");
            ViewData["Tsubscrid"] = new SelectList(_context.Subscriptions, "Subscrid", "Subscrid");
            return View();
        }

        // POST: Typepersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tprofileid,Tsubscrid,Startdate,Enddate,Status,Id")] Typeperson typeperson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeperson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", typeperson.Tprofileid);
            ViewData["Tsubscrid"] = new SelectList(_context.Subscriptions, "Subscrid", "Subscrid", typeperson.Tsubscrid);
            return View(typeperson);
        }

        // GET: Typepersons/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Typepeople == null)
            {
                return NotFound();
            }

            var typeperson = await _context.Typepeople.FindAsync(id);
            if (typeperson == null)
            {
                return NotFound();
            }
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", typeperson.Tprofileid);
            ViewData["Tsubscrid"] = new SelectList(_context.Subscriptions, "Subscrid", "Subscrid", typeperson.Tsubscrid);
            return View(typeperson);
        }

        // POST: Typepersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Tprofileid,Tsubscrid,Startdate,Enddate,Status,Id")] Typeperson typeperson)
        {
            if (id != typeperson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypepersonExists(typeperson.Id))
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
            ViewData["Tprofileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", typeperson.Tprofileid);
            ViewData["Tsubscrid"] = new SelectList(_context.Subscriptions, "Subscrid", "Subscrid", typeperson.Tsubscrid);
            return View(typeperson);
        }

        // GET: Typepersons/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Typepeople == null)
            {
                return NotFound();
            }

            var typeperson = await _context.Typepeople
                .Include(t => t.Tprofile)
                .Include(t => t.Tsubscr)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeperson == null)
            {
                return NotFound();
            }

            return View(typeperson);
        }

        // POST: Typepersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Typepeople == null)
            {
                return Problem("Entity set 'ModelContext.Typepeople'  is null.");
            }
            var typeperson = await _context.Typepeople.FindAsync(id);
            if (typeperson != null)
            {
                _context.Typepeople.Remove(typeperson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypepersonExists(decimal id)
        {
          return (_context.Typepeople?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
