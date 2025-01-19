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



            var authuperson = _context.Profiles
                .Where(x => x.Username.ToLower().Trim() == profile.Username.ToLower().Trim() && x.Userpassword == profile.Userpassword)
                .SingleOrDefault();
            var Rname = _context.Roles.Where(x=>x.Roleid == authuperson.Roleid).FirstOrDefault();

            if (authuperson != null)
            {
                try
                {
                  
                    HttpContext.Session.SetString("UserPhoto", authuperson.Photo ?? "default_photo.png"); 
                    HttpContext.Session.SetString("UserNameandLastname", $"{authuperson.Name} {authuperson.Lname}");
                    HttpContext.Session.SetString("UserRoleName", Rname.Rname ?? "Unknown Role");
                    HttpContext.Session.SetInt32("UserID", (int)authuperson.Profileid);
                    HttpContext.Session.SetInt32("UserRoleID", (int)authuperson.Roleid);
				


					HttpContext.Session.SetInt32("UserIsEnter", 1);

                    switch (authuperson.Roleid)
                    {
                        case 1:
                            return RedirectToAction("Index", "Admin");

                        case 2:
                            return RedirectToAction("listProfileMembar", "Trainer");

                        case 3:
                            return RedirectToAction("Index", "Home");

                        default:
                            ViewBag.UserIsEnter = false;
                            return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                    return View("loginAndRegister");
                }
            }
                    

                ViewBag.ErrorMessage = "User Name or Password is not correct.";
            return View("loginAndRegister");
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
