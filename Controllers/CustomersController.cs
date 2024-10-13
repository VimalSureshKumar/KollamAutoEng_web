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
    public class CustomersController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public CustomersController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Customers
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Store the current sort order for use in the view
            ViewData["CurrentSort"] = sortOrder;

            // Set up sort parameters for last name sorting
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "last_name_desc" : "LastName";

            // If a new search is performed, reset the page number to 1
            if (searchString != null)
            {
                pageNumber = 1; // Reset to the first page when a new search is made
            }
            else
            {
                // If no new search is made, retain the current filter string
                searchString = currentFilter;
            }

            // Check if the Customer context is null
            if (_context.Customer == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Customer' is null."); // Return an error if it is
            }

            // Store the current search string for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Query to retrieve customers
            var customers = from cus in _context.Customer
                            select cus;

            // If the search string is not empty, filter the customers based on various fields
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(m =>
                    m.FirstName.Contains(searchString) || // Search by customer's first name
                    m.LastName.Contains(searchString) ||  // Search by customer's last name
                    (m.FirstName + " " + m.LastName).Contains(searchString) || // Search by full customer name
                    m.Email.Contains(searchString) || // Search by email
                    m.PhoneNumber.Contains(searchString) // Search by phone number
                );
            }

            // Sort customers based on the selected sort order
            switch (sortOrder)
            {
                case "LastName":
                    customers = customers.OrderBy(c => c.LastName); // Ascending order
                    break;
                case "last_name_desc":
                    customers = customers.OrderByDescending(c => c.LastName); // Descending order
                    break;
            }

            int pageSize = 10; // Define the number of items per page
                               // Return the paginated list of customers to the view
            return View(await PaginatedList<Customer>.CreateAsync(customers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Customers/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin,Employee,User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee,User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,PhoneNumber,Gender,DateOfBirth")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Vehicles", new { customerId = customer.CustomerId });
            }
            return View(customer);
        }

        // GET: Customers/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,PhoneNumber,Gender,DateOfBirth")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customers/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Customer' is null.");
            }

            var customer = await _context.Customer
                .Include(c => c.Vehicles)
                .Include(c => c.Appointments)
                .ThenInclude(a => a.FaultParts) 
                .Include(c => c.Faults)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer != null)
            {
                foreach (var appointment in customer.Appointments)
                {
                    if (appointment.FaultParts?.Any() == true)
                    {
                        _context.FaultPart.RemoveRange(appointment.FaultParts);
                    }
                }

                if (customer.Faults?.Any() == true)
                {
                    _context.Fault.RemoveRange(customer.Faults);
                }

                if (customer.Appointments?.Any() == true)
                {
                    _context.Appointment.RemoveRange(customer.Appointments);
                }

                if (customer.Vehicles?.Any() == true)
                {
                    _context.Vehicle.RemoveRange(customer.Vehicles);
                }

                if (customer.Payments?.Any() == true)
                {
                    _context.Payment.RemoveRange(customer.Payments);
                }

                _context.Customer.Remove(customer);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customer?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}