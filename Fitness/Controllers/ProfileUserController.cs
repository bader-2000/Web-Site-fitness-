using Fitness.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
			return View();
		}

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

                String wwwRootPath = _webHostEnvironment.WebRootPath;
                String filename = Guid.NewGuid().ToString() + "_" + profile.ImageFile.FileName;
                String path = Path.Combine(wwwRootPath + "/images/" + filename);

                using (var filestrem = new FileStream(path, FileMode.Create))
                {
                    await profile.ImageFile.CopyToAsync(filestrem);
                }
                profile.Photo = filename;

                try
                {
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
            return View(profile);
        }
        private bool ProfileExists(decimal id)
{
  return (_context.Profiles?.Any(e => e.Profileid == id)).GetValueOrDefault();
}
    }
}
