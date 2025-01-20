using Fitness.Models;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using iText.Layout;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Syncfusion.Office;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using iText.Layout.Properties;

namespace Fitness.Controllers
{
    public class UserPaymentController : Controller
    {

        private readonly ModelContext _context;

        
      
        public UserPaymentController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // عرض الخطط المتاحة
        public async Task<IActionResult> ChooseWQplan()
        {
            var workoutplans = await _context.Workoutplans.ToListAsync();
            return workoutplans != null ? View(workoutplans) : Problem("Entity set 'ModelContext.Workoutplans' is null.");
        }


        // عرض صفحة إضافة بطاقة الدفع
        public IActionResult AddVisaCard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseMembership(string NamePlan, decimal Price)
        {
            if (HttpContext.Session.GetInt32("UserIsEnter") == 1)
            {
                TempData["NamePlan"] = NamePlan;
                TempData["Price"] = Price.ToString(); // تحويل إلى string
                return RedirectToAction("ChooseWQplan", "UserPayment");
            }
            else
            {
                return RedirectToAction("LoginAndRegister", "Auth");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseWQplan(decimal? id, decimal countweek)
        {
            if (id == null || _context.Workoutplans == null)
            {
                return NotFound();
            }

            TempData["ID"] = id.ToString(); // تحويل إلى string
            TempData["CountWeek"] = countweek.ToString(); // تحويل إلى string

            return RedirectToAction("AddVisaCard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVisaCard([Bind("Paymentid,Amount,Paymentdate,Cardnumber,Cardholdername,Expirydate,Profileid")] Payment payment)
        {
            var UserID = Convert.ToDecimal(HttpContext.Session.GetInt32("UserID"));
            payment.Profileid = UserID;

            if (ModelState.IsValid)
            {
                try
                {
                    // استرجاع القيم من TempData
                    var NamePlan = TempData["NamePlan"] as string;
                    var Price = Convert.ToDecimal(TempData["Price"]); // تحويل إلى decimal
                    var ID = Convert.ToDecimal(TempData["ID"]); // تحويل إلى decimal
                    var CountWeek = Convert.ToDecimal(TempData["CountWeek"]); // تحويل إلى decimal

                    // إنشاء الاشتراك
                    var subscription = new Subscription
                    {
                        Nameplan = NamePlan,
                        Price = Price,
                        Sidwop = ID,
                        Countweeks = CountWeek
                    };

                    _context.Subscriptions.Add(subscription);
                    await _context.SaveChangesAsync();

                    // استرجاع الـ Id بعد الإضافة
                    var Subscrid = subscription.Subscrid;

                    // إضافة نوع الشخص
                    var typeperson = new Typeperson
                    {
                        Startdate = DateTime.Now,
                        Enddate = DateTime.Now.AddMonths(1),
                        Status = "Active",
                        Tprofileid = UserID,
                        Tsubscrid = Subscrid
                    };

                    _context.Typepeople.Add(typeperson);
                    await _context.SaveChangesAsync();

                    // إضافة الدفع
                    _context.Add(payment);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("AddVisaCard");
                }
                catch (DbUpdateException dbEx)
                {
                    Console.WriteLine("Inner Exception: " + dbEx.InnerException?.Message);
                    Console.WriteLine("Exception: " + dbEx.Message);
                    ViewBag.error = dbEx.InnerException?.Message ?? dbEx.Message;
                    return View(payment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    ViewBag.error = ex.Message;
                    return View(payment);
                }
            }

            return View("AddVisaCard");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult SendPdf()
        {
            // الحصول على بيانات المستخدم
            var UserID = Convert.ToDecimal(HttpContext.Session.GetInt32("UserID") ?? 3);
            var User = _context.Profiles.FirstOrDefault(x => x.Profileid == UserID);
            var TypePersone = _context.Typepeople.FirstOrDefault(x => x.Tprofileid == UserID);

            if (User == null || TypePersone == null)
            {
                return Content("User or subscription details not found.");
            }

            var IDSubscr = _context.Subscriptions
                .Include(s => s.SidwopNavigation)
                .FirstOrDefault(x => x.Subscrid == TypePersone.Tsubscrid);

            if (IDSubscr == null)
            {
                return Content("Subscription details not found.");
            }

            byte[] pdfBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // إنشاء ملف PDF
                document.Add(new Paragraph("Invoice")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(18));

                document.Add(new Paragraph("Thank you for your subscription to Fitness Gym.")
                    .SetFontSize(14));

                document.Add(new Paragraph("Subscription Details")
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetFontSize(14));

                Table table = new Table(2, true);
                table.AddHeaderCell("Information");
                table.AddHeaderCell("Details");

                table.AddCell("Name");
                table.AddCell($"{User.Name} {User.Lname}");

                table.AddCell("Plan Name");
                table.AddCell($"{IDSubscr.Nameplan}");

                table.AddCell("Start Date");
                table.AddCell($"{TypePersone.Startdate:yyyy-MM-dd}");

                table.AddCell("End Date");
                table.AddCell($"{TypePersone.Enddate:yyyy-MM-dd}");

                table.AddCell("Status");
                table.AddCell($"{TypePersone.Status}");

                table.AddCell("Price");
                table.AddCell($"${IDSubscr.Price}");

                document.Add(table);
                document.Close();

                pdfBytes = ms.ToArray();
            }

            // إعداد البريد الإلكتروني
            string fromEmail = "bader.sleman2000@gmail.com";
            string toEmail = User.Email;
            string subject = "Payment Confirmation - Fitness Gym";
            string body = $@"
Dear {User.Name},

We are pleased to inform you that your payment has been successfully processed!

Thank you for subscribing to Fitness Gym. We are excited to have you as a part of our community. Below are your subscription details:

- **Plan Name**: {IDSubscr.Nameplan}
- **Start Date**: {TypePersone.Startdate:yyyy-MM-dd}
- **End Date**: {TypePersone.Enddate:yyyy-MM-dd}
- **Status**: {TypePersone.Status}
- **Price**: ${IDSubscr.Price}

We look forward to supporting you on your fitness journey!

Best regards,  
**Fitness Gym Team**
";

            using (MailMessage mail = new MailMessage(fromEmail, toEmail))
            {
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = false;

                // إضافة ملف PDF كمرفق
                mail.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "Invoice.pdf"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(fromEmail, "bnot ucvx aahn klgr");
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

            TempData["SuccessMessage"] = "The email with the invoice has been sent successfully!";
            return RedirectToAction("MyVisaCard");
        }

    }
}

