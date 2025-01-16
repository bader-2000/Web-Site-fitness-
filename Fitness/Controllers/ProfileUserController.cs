using Fitness.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Controllers

{
	public class ProfileUserController : Controller
	{
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;   

		public ProfileUserController(ModelContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
            _webHostEnvironment = webHostEnvironment;
		}
        
        
        public IActionResult Index()
        {
            var profiles = _context.Profiles.Include(p => p.Role).ToList();
            return View(profiles);
        }

        public async Task<IActionResult> Details(decimal? id = 1)
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

   
        public async Task<IActionResult> Edit(decimal? id = 1)
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

            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rname", profile.Roleid);
            return View(profile);
        }




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
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string filename = Guid.NewGuid().ToString() + "_" + profile.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/images/" + filename);

                        using (var filestrem = new FileStream(path, FileMode.Create))
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
                return RedirectToAction(nameof(Details), new { id = profile.Profileid });
            }

         
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rname", profile.Roleid);
            return View(profile);
        }

        private bool ProfileExists(decimal id)
        {
            return (_context.Profiles?.Any(e => e.Profileid == id)).GetValueOrDefault();
        }
    }
}


