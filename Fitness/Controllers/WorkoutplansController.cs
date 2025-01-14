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
    public class WorkoutplansController : Controller
    {
        private readonly ModelContext _context;

        public WorkoutplansController(ModelContext context)
        {
            _context = context;
        }

        // GET: Workoutplans
        public async Task<IActionResult> Index()
        {
              return _context.Workoutplans != null ? 
                          View(await _context.Workoutplans.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Workoutplans'  is null.");
        }

        // GET: Workoutplans/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Workoutplans == null)
            {
                return NotFound();
            }

            var workoutplan = await _context.Workoutplans
                .FirstOrDefaultAsync(m => m.Idwop == id);
            if (workoutplan == null)
            {
                return NotFound();
            }

            return View(workoutplan);
        }

        // GET: Workoutplans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workoutplans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idwop,Numberofweek,Goals,Day1,Day2,Day3,Day4,Day5,Day6,Day7")] Workoutplan workoutplan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutplan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutplan);
        }

        // GET: Workoutplans/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Workoutplans == null)
            {
                return NotFound();
            }

            var workoutplan = await _context.Workoutplans.FindAsync(id);
            if (workoutplan == null)
            {
                return NotFound();
            }
            return View(workoutplan);
        }

        // POST: Workoutplans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Idwop,Numberofweek,Goals,Day1,Day2,Day3,Day4,Day5,Day6,Day7")] Workoutplan workoutplan)
        {
            if (id != workoutplan.Idwop)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutplan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutplanExists(workoutplan.Idwop))
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
            return View(workoutplan);
        }

        // GET: Workoutplans/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Workoutplans == null)
            {
                return NotFound();
            }

            var workoutplan = await _context.Workoutplans
                .FirstOrDefaultAsync(m => m.Idwop == id);
            if (workoutplan == null)
            {
                return NotFound();
            }

            return View(workoutplan);
        }

        // POST: Workoutplans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Workoutplans == null)
            {
                return Problem("Entity set 'ModelContext.Workoutplans'  is null.");
            }
            var workoutplan = await _context.Workoutplans.FindAsync(id);
            if (workoutplan != null)
            {
                _context.Workoutplans.Remove(workoutplan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutplanExists(decimal id)
        {
          return (_context.Workoutplans?.Any(e => e.Idwop == id)).GetValueOrDefault();
        }
    }
}
