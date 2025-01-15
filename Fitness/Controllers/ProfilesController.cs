using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Fitness.Models;

namespace FitnessWebApplication1.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfilesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Profiles
     
                public async Task<IActionResult> Index()
        {
            var modelContext = _context.Profiles.Include(p => p.Role);
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
             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Rname");
            return View();
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
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Rname", profile.Roleid); // إعادة تعبئة القائمة
            return View(profile);
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
             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Roleid", profile.Roleid);
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Profileid,Name,Username,Userpassword,ImageFile,DateOfBirth,Email,Lname")] Profile profile)
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
                return RedirectToAction(nameof(Index));
            }
             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Rname", profile.Roleid); // إعادة تعبئة القائمة
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
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(decimal id)
        {
            return (_context.Profiles?.Any(e => e.Profileid == id)).GetValueOrDefault();
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using testfiness.Models;

//namespace testfiness.Controllers
//{
//    public class ProfilesController : Controller
//    {
//        private readonly ModelContext _context;

//        public ProfilesController(ModelContext context)
//        {
//            _context = context;
//        }

//        // GET: Profiles
//        public async Task<IActionResult> Index()
//        {
//            var modelContext = _context.Profiles.Include(p => p.Role);
//            return View(await modelContext.ToListAsync());
//        }

//        // GET: Profiles/Details/5
//        public async Task<IActionResult> Details(decimal? id)
//        {
//            if (id == null || _context.Profiles == null)
//            {
//                return NotFound();
//            }

//            var profile = await _context.Profiles
//                .Include(p => p.Role)
//                .FirstOrDefaultAsync(m => m.Profileid == id);
//            if (profile == null)
//            {
//                return NotFound();
//            }

//            return View(profile);
//        }

//        // GET: Profiles/Create
//        public IActionResult Create()
//        {
//             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Roleid");
//            return View();
//        }

//        // POST: Profiles/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Profileid,Name,Username,Userpassword,Photo,DateOfBirth,Email,Lname,Roleid")] Profile profile)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(profile);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Roleid", profile.Roleid);
//            return View(profile);
//        }

//        // GET: Profiles/Edit/5
//        public async Task<IActionResult> Edit(decimal? id)
//        {
//            if (id == null || _context.Profiles == null)
//            {
//                return NotFound();
//            }

//            var profile = await _context.Profiles.FindAsync(id);
//            if (profile == null)
//            {
//                return NotFound();
//            }
//             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Roleid", profile.Roleid);
//            return View(profile);
//        }

//        // POST: Profiles/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(decimal id, [Bind("Profileid,Name,Username,Userpassword,Photo,DateOfBirth,Email,Lname,Roleid")] Profile profile)
//        {
//            if (id != profile.Profileid)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(profile);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ProfileExists(profile.Profileid))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//             ViewBag.Roleid = new SelectList(_context.Roles, "Roleid", "Roleid", profile.Roleid);
//            return View(profile);
//        }

//        // GET: Profiles/Delete/5
//        public async Task<IActionResult> Delete(decimal? id)
//        {
//            if (id == null || _context.Profiles == null)
//            {
//                return NotFound();
//            }

//            var profile = await _context.Profiles
//                .Include(p => p.Role)
//                .FirstOrDefaultAsync(m => m.Profileid == id);
//            if (profile == null)
//            {
//                return NotFound();
//            }

//            return View(profile);
//        }

//        // POST: Profiles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(decimal id)
//        {
//            if (_context.Profiles == null)
//            {
//                return Problem("Entity set 'ModelContext.Profiles'  is null.");
//            }
//            var profile = await _context.Profiles.FindAsync(id);
//            if (profile != null)
//            {
//                _context.Profiles.Remove(profile);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ProfileExists(decimal id)
//        {
//            return (_context.Profiles?.Any(e => e.Profileid == id)).GetValueOrDefault();
//        }
//    }
//}
