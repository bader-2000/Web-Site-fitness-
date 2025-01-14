using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Controllers
{
    public class TranierController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TranierController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        

        public async Task<IActionResult> listProfileMembar()
        {
            var modelContext = _context.Profiles
                .Include(p => p.Role)
                .Where(p => p.Role.Roleid == 3);
            return View(await modelContext.ToListAsync());
        }
        // GET: Workoutplans
        public async Task<IActionResult> SeeAllPlan()
        {
            return _context.Workoutplans != null ?
                        View(await _context.Workoutplans.ToListAsync()) :
                        Problem("Entity set 'ModelContext.Workoutplans'  is null.");
        }




        // GET: Workoutplans/Create 
        public IActionResult CreateNewPlan()
        {
            return View();
        }

        // POST: Workoutplans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewPlan([Bind("Idwop,Numberofweek,Goals,Day1,Day2,Day3,Day4,Day5,Day6,Day7")] Workoutplan workoutplan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutplan);
                await _context.SaveChangesAsync();
                return RedirectToAction("SeeAllPlan");
            }
            return View();
        }








        // GET: Workoutplans/Edit/5
        public async Task<IActionResult> UpdatePlan(decimal? id)
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
        public async Task<IActionResult> UpdatePlan(decimal id, [Bind("Idwop,Numberofweek,Goals,Day1,Day2,Day3,Day4,Day5,Day6,Day7")] Workoutplan workoutplan)
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
                return RedirectToAction("SeeAllPlan");
            }
            return View(workoutplan);
        }












        // POST: Workoutplans/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
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
            return RedirectToAction("SeeAllPlan");
        }

        private bool WorkoutplanExists(decimal id)
        {
            return (_context.Workoutplans?.Any(e => e.Idwop == id)).GetValueOrDefault();
        }
    }
}
