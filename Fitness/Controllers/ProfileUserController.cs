using Fitness.Models;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iText.Layout;
using System.Net.Mail;
using System.Net;
using iText.Layout.Properties;


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



        public IActionResult SendPdf()
        {
            var UserID = Convert.ToDecimal(HttpContext.Session.GetInt32("UserID") ?? 3);
            var User = _context.Profiles.Where(x => x.Profileid == UserID).FirstOrDefault();
            var TypePersone = _context.Typepeople.Where(x => x.Tprofileid == UserID).FirstOrDefault();
            var IDSubscr = _context.Subscriptions.Include(s => s.SidwopNavigation).FirstOrDefault(x => x.Subscrid == TypePersone.Tsubscrid);
            var Idplan = _context.Subscriptions.Include(x => x.Sidwop == IDSubscr.Sidwop);

            byte[] pdfBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                
               
                document.Add(new Paragraph("Invoice")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18));

                document.Add(new Paragraph($"We appreciate your business with Finess Gym. Below are the details of your recent purchase.")
                 .SetFontSize(14));
                document.Add(new Paragraph($"----------------Your subscription details-------------------")
                   .SetFontSize(14));
               
               
                Table table = new Table(2, true);
                table.AddHeaderCell("information");
                table.AddHeaderCell("Custmer");


                table.AddCell("Name");
                table.AddCell($"{User.Name}{User.Lname}");
               

                table.AddCell("Name Plan");
                table.AddCell($"{IDSubscr.Nameplan}");

                table.AddCell("Start Date");
                table.AddCell($"{TypePersone.Startdate}");

                table.AddCell("End Date");
                table.AddCell($"{TypePersone.Enddate}");

                table.AddCell("Status");
                table.AddCell($"{TypePersone.Status}");

                table.AddCell("Price");
                table.AddCell($"${IDSubscr.Price}");
               
               
                document.Add(table);
                document.Add(new Paragraph("Hello, this is Email by Fitness Gyms "  ));
                document.Close();
             
                pdfBytes = ms.ToArray();
            }

           
            string fromEmail = "bader.sleman2000@gmail.com";
            string toEmail = $"{User.Email}";
            string subject = "Your PDF File";
            string body = "Please find the attached PDF file.";

            using (MailMessage mail = new MailMessage(fromEmail, toEmail))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

               
                mail.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "Sample.pdf"));

              
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) 
                {
                 
                    smtp.Credentials = new NetworkCredential("bader.sleman2000@gmail.com", "bnot ucvx aahn klgr"); 
                    smtp.EnableSsl = true;  

                    try
                    {
                       
                        smtp.Send(mail);
                    }
                    catch (SmtpException ex)
                    { 
                        return Content($"Error sending email: {ex.Message}");
                    }
                }
            }

            TempData["SuccessMessage"] = "Success Send You Email!";
            return RedirectToAction("MyVisaCard");
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

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> MySubscription(decimal? id = 3)
        {
            if (id == null || _context.Subscriptions == null)
            {
                
                return View(); 
            }

            var typePerson = await _context.Typepeople
                .Include(tp => tp.Tprofile)
                .FirstOrDefaultAsync(tp => tp.Tprofile != null && tp.Tprofile.Profileid == id.Value);

            if (typePerson != null)
            {
                ViewBag.MoreDetils = typePerson;

                var subscription = await _context.Subscriptions
                    .Include(s => s.SidwopNavigation)
                    .FirstOrDefaultAsync(s => s.Subscrid == typePerson.Tsubscrid);

                if (subscription != null)
                {
                    var plan = await _context.Workoutplans
                        .FirstOrDefaultAsync(wp => wp.Idwop == subscription.Sidwop);

                    if (plan != null)
                    {
                        ViewBag.Plan = plan;
                        return View(subscription); 
                    }
                }
            }

           
            return View();
        }


        public async Task<IActionResult> MyVisaCard(decimal? id = 3)
        {
            if (id == null || _context.Payments == null)
            {
                return View();
            }

            var payment = _context.Payments.Where(p => p.Profileid == Convert.ToDecimal(id)).FirstOrDefault();
            if (payment == null)
            {
                return View();
            }

            return View(payment);
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


