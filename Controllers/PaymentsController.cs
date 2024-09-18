using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Areas.Identity.Data;
using KollamAutoEng_web.Models;
using Microsoft.AspNetCore.Authorization;

namespace KollamAutoEng_web.Controllers
{
    [Authorize(Roles = "Admin,Employee,User")]
    public class PaymentsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public PaymentsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Payments
        [Authorize(Roles = "Admin,Employee,User")]
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Payment' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var payments = from pay in _context.Payment
                           .Include(m => m.Customer)
                           select pay;

            if (!String.IsNullOrEmpty(searchString))
            {
                payments = payments.Where(m =>
                    m.Customer.FirstName.Contains(searchString) ||
                    m.Customer.LastName.Contains(searchString) 
                );
            }

            int pageSize = 10;
            return View(await PaginatedList<Payment>.CreateAsync(payments.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Payments/Details
        [Authorize(Roles = "Admin,Employee,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,Amount,PaymentDate,PaymentMethod,CustomerId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", payment.CustomerId);
            return View(payment);
        }

        // GET: Payments/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", payment.CustomerId);
            return View(payment);
        }

        // POST: Payments/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Amount,PaymentDate,PaymentMethod,CustomerId")] Payment payment)
        {
            if (id != payment.PaymentId)
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
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", payment.CustomerId);
            return View(payment);
        }

        // GET: Payments/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Payment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Payment'  is null.");
            }
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payment?.Any(e => e.PaymentId == id)).GetValueOrDefault();
        }
    }
}
