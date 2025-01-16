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
    public class PaymentsController : Controller
    {
        private readonly ModelContext _context;

        public PaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Payments.Include(p => p.Profile);
            return View(await modelContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Profile)
                .FirstOrDefaultAsync(m => m.Paymentid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["Profileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Paymentid,Amount,Paymentdate,Cardnumber,Cardholdername,Expirydate,Profileid")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // If an error occurs while saving, add it to ModelState
                    ModelState.AddModelError("", "An error occurred while adding the data: " + ex.Message);
                }
            }
            else
            {
                // Add a generic error message if model validation fails
                ModelState.AddModelError("", "Please correct the errors below.");
            }

            // Rebind Profileid for the dropdown in case of validation failure
            ViewBag.Profileid = new SelectList(_context.Profiles, "Profileid", "Profileid", payment.Profileid);
            return View(payment); // Pass the model back to the view
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Paymentid,Amount,Paymentdate,Cardnumber,Cardholdername,Expirydate,Profileid")] Payment payment)
        {
            if (id != payment.Paymentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Paymentid))
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
            ViewData["Profileid"] = new SelectList(_context.Profiles, "Profileid", "Profileid", payment.Profileid);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Profile)
                .FirstOrDefaultAsync(m => m.Paymentid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Payments == null)
            {
                return Problem("Entity set 'ModelContext.Payments'  is null.");
            }
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(decimal id)
        {
          return (_context.Payments?.Any(e => e.Paymentid == id)).GetValueOrDefault();
        }
    }
}
