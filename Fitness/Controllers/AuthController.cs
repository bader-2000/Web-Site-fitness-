using Fitness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Controllers
{
    public class AuthController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Profiles / Create                                                  
        public IActionResult loginAndRegister()
        {
            
			HttpContext.Session.SetInt32("UserIsEnter", 0);
            return View();
        }

        public IActionResult login([Bind("Username,Userpassword")] Profile profile)
        {
           

                var authuperson = _context.Profiles.Where(x => x.Username == profile.Username && x.Userpassword == profile.Userpassword).SingleOrDefault();


            if (authuperson != null)
            {
                try
                {
                    HttpContext.Session.SetInt32("UserID",(Int32)authuperson.Profileid);
                    HttpContext.Session.SetInt32("UserRoleID",(Int32)authuperson.Roleid);
                    switch (authuperson.Roleid)
                    {
                        case 1:

                            HttpContext.Session.SetInt32("UserIsEnter", 1);

                            return RedirectToAction("Index", "Admin");

                        case 2:
                            HttpContext.Session.SetInt32("UserIsEnter", 1);
                            return RedirectToAction("Index", "Tranier");

                        case 3:
                            HttpContext.Session.SetInt32("UserIsEnter", 1);
                            return RedirectToAction("Index", "Home");

                        default:
                            ViewBag.UserIsEnter = false;
                            return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    ViewBag.ErrorMessage = "User Name Or Password is Not Correct";
                }
            }   
         
			ViewBag.ErrorMessage = "User Name Or Password is Not Correct";
            return RedirectToAction("loginAndRegister");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Profileid,Name,Lname,,Email,Username,Userpassword,ImageFile,DateOfBirth,RoleId")] Profile profile)
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
                profile.Roleid = 3;
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction("loginAndRegister");
            }


             return RedirectToAction("loginAndRegister");
        }


    }


}
