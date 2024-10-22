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
    [Authorize(Roles = "Admin,Employee")] // Restrict access to users with Admin or Employee roles
    public class PaymentsController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context for accessing payment data

        // Constructor that initializes the context
        public PaymentsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Payments
        [Authorize(Roles = "Admin,Employee")] // Restrict access to users with Admin or Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set the sort parameter for customers based on the current sort order
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer";

            // Check if the search string has been modified; reset the page number if it has
            if (searchString != null)
            {
                pageNumber = 1; // Reset page number to 1 if searching
            }
            else
            {
                searchString = currentFilter; // Preserve the current filter for pagination
            }

            // Check if the Payment context is null
            if (_context.Payment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Payment' is null."); // Return an error if it is null
            }

            // Store the current search string in ViewData for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Retrieve payments including the associated customers
            var payments = from pay in _context.Payment
                           .Include(m => m.Customer) // Include related customer data
                           select pay;

            // Filter payments based on the search string, checking first and last names of customers
            if (!String.IsNullOrEmpty(searchString))
            {
                payments = payments.Where(m =>
                    m.Customer.FirstName.Contains(searchString) || // Search by customer's first name
                    m.Customer.LastName.Contains(searchString) || // Search by customer's last name
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString) // Full name search
                );
            }

            // Sorting logic based on the selected sort order
            switch (sortOrder)
            {
                case "Customer":
                    payments = payments.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName); // Ascending order
                    break;
                case "customer_desc":
                    payments = payments.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName); // Descending order
                    break;
            }

            int pageSize = 10; // Define the number of items per page
            // Return the paginated list of payments to the view
            return View(await PaginatedList<Payment>.CreateAsync(payments.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Payments/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or if the Payment context is null
            if (id == null || _context.Payment == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            // Retrieve the payment details along with the associated customer
            var payment = await _context.Payment
                .Include(p => p.Customer) // Include related customer data
                .FirstOrDefaultAsync(m => m.PaymentId == id); // Find payment by id
            if (payment == null)
            {
                return NotFound(); // Return NotFound if payment does not exist
            }

            return View(payment); // Return the payment details view
        }

        // GET: Payments/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            // Populate the dropdown for customers
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            return View(); // Return the Create view
        }

        // POST: Payments/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,Amount,PaymentDate,PaymentMethod,CustomerId")] Payment payment)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Check for existing payments for the same customer, date, and amount
                var existingPayment = await _context.Payment
                    .FirstOrDefaultAsync(p => p.CustomerId == payment.CustomerId &&
                                               p.PaymentDate == payment.PaymentDate && // Compare dates
                                               p.Amount == payment.Amount);

                if (existingPayment != null) // If a duplicate payment is found
                {
                    // Add an error message to the model state
                    ModelState.AddModelError("PaymentDate", "A payment with the same amount for this customer already exists on the selected date.");
                }
                else
                {
                    _context.Add(payment); // Add the new payment to the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the Index action
                }
            }

            // If we reach this point, something failed; re-populate the view data for the dropdowns
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", payment.CustomerId);
            return View(payment); // Return the view with validation errors
        }

        // GET: Payments/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null or if the Payment context is null
            if (id == null || _context.Payment == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            // Find the payment by id
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound(); // Return NotFound if payment does not exist
            }

            // Populate the dropdown for customers
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", payment.CustomerId);
            return View(payment); // Return the Edit view
        }

        // POST: Payments/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Amount,PaymentDate,PaymentMethod,CustomerId")] Payment payment)
        {
            // Check if the payment id matches the passed id
            if (id != payment.PaymentId)
            {
                return NotFound(); // Return NotFound if ids do not match
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment); // Update the payment in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issues
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound(); // Return NotFound if payment does not exist
                    }
                    else
                    {
                        throw; // Throw exception if another error occurs
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }

            // If we reach this point, something failed; re-populate the view data for the dropdowns
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", payment.CustomerId);
            return View(payment); // Return the view with validation errors
        }

        // GET: Payments/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or if the Payment context is null
            if (id == null || _context.Payment == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            // Retrieve the payment details along with the associated customer
            var payment = await _context.Payment
                .Include(p => p.Customer) // Include related customer data
                .FirstOrDefaultAsync(m => m.PaymentId == id); // Find payment by id
            if (payment == null)
            {
                return NotFound(); // Return NotFound if payment does not exist
            }

            return View(payment); // Return the Delete confirmation view
        }

        // POST: Payments/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the Payment context is null
            if (_context.Payment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Payment' is null."); // Return an error if it is null
            }

            // Find the payment by id
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment); // Remove the payment from the context
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the Index action
        }

        // Check if a payment exists by id
        private bool PaymentExists(int id)
        {
            return (_context.Payment?.Any(e => e.PaymentId == id)).GetValueOrDefault(); // Return true if payment exists, otherwise false
        }
    }
}
