using Fitness.Models;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


using System.IO;
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult choooseMembership(String NamePlan ,int Price = 10 )
        {

            //if (HttpContext.Session.GetString("UserIsEnter") == "0")
            //{
            ViewBag.Price = Price;



            return RedirectToAction("ChooseWQplan", "UserPayment");


            //}
            //else
            //{
            //    return RedirectToAction("loginAndRegister", "Auth");
            //}
        }

        //get plan
        public async Task<IActionResult> ChooseWQplan()
        {

            return _context.Workoutplans != null ?
                             View(await _context.Workoutplans.ToListAsync()) :
                             Problem("Entity set 'ModelContext.Workoutplans'  is null.");



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseWQplan(int Idwop)
        {


            if (ModelState.IsValid)
            {

                var Idwops = Idwop;

            }



            return RedirectToAction("AddVisaCard");


        }




        //public async Task<IActionResult> AddVisaCardisFind(decimal? Profileid = 3 )
        //{

        //    if (Profileid == null || _context.Payments == null)
        //    {
        //        return RedirectToAction("AddVisaCard"); 
        //    }

        //    var payment =  _context.Payments.Where(x=>x.Profileid == Profileid ).FirstAsync();
        //    if (payment == null)
        //    {
        //        return RedirectToAction("AddVisaCard");
        //    }
        //   // ViewData["Profileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", payment.Profileid);
        //    return View(payment);


        //}

        // get plan
        public async Task<IActionResult> AddVisaCard()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVisaCard([Bind("Paymentid,Amount,Paymentdate,Cardnumber,Cardholdername,Expirydate,Profileid")] Payment payment)
        {
           
            payment.Profileid = 3;

           
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Add(payment);
                    await _context.SaveChangesAsync();

                  
                    return RedirectToAction("PaymentSuccess"); 
                }
                catch (Exception ex)
                {
                    
                    ViewBag.error = ex.Message;
                    return View(payment); 
                }
            }
 
           
            return View(payment);
        }


          
       


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
