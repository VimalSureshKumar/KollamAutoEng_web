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
    [Authorize(Roles = "Admin,Employee,User")] // Authorize access for Admin, Employee, and User roles
    public class CustomersController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context for accessing data

        public CustomersController(KollamAutoEng_webContext context)
        {
            _context = context; // Initialize the context
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
            // Check if the ID is null or if the Customer context is null
            if (id == null || _context.Customer == null)
            {
                return NotFound(); // Return NotFound if no ID is provided or the context is null
            }

            // Retrieve the customer with the specified ID
            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound(); // Return NotFound if the customer doesn't exist
            }

            return View(customer); // Return the details view for the customer
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin,Employee,User")]
        public IActionResult Create()
        {
            return View(); // Return the Create view
        }

        // POST: Customers/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee,User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,PhoneNumber,Gender,DateOfBirth")] Customer customer)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                // Check for existing customers with the same Email or PhoneNumber
                var existingCustomer = await _context.Customer
                    .FirstOrDefaultAsync(c => c.Email == customer.Email || c.PhoneNumber == customer.PhoneNumber);

                if (existingCustomer != null) // If a duplicate is found
                {
                    // Add an error message to the model state
                    ModelState.AddModelError("Email", "A customer with the same email or phone number already exists.");
                }
                else
                {
                    _context.Add(customer); // Add the new customer to the context
                    await _context.SaveChangesAsync(); // Save changes to the database

                    return RedirectToAction("Create", "Vehicles", new { customerId = customer.CustomerId }); // Redirect to the Vehicles creation
                }
            }
            return View(customer); // Return the view with validation errors
        }

        // GET: Customers/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the ID is null or if the Customer context is null
            if (id == null || _context.Customer == null)
            {
                return NotFound(); // Return NotFound if no ID is provided or the context is null
            }

            // Retrieve the customer with the specified ID
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound(); // Return NotFound if the customer doesn't exist
            }
            return View(customer); // Return the Edit view for the customer
        }

        // POST: Customers/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,PhoneNumber,Gender,DateOfBirth")] Customer customer)
        {
            // Check if the provided ID matches the customer ID
            if (id != customer.CustomerId)
            {
                return NotFound(); // Return NotFound if the IDs do not match
            }

            if (ModelState.IsValid) // Check if the model state is valid
            {
                try
                {
                    _context.Update(customer); // Update the customer in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handle concurrency exceptions
                {
                    if (!CustomerExists(customer.CustomerId)) // Check if the customer still exists
                    {
                        return NotFound(); // Return NotFound if the customer does not exist
                    }
                    else
                    {
                        throw; // Re-throw the exception if it is a different issue
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }
            return View(customer); // Return the view with validation errors
        }

        // GET: Customers/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the ID is null or if the Customer context is null
            if (id == null || _context.Customer == null)
            {
                return NotFound(); // Return NotFound if no ID is provided or the context is null
            }

            // Retrieve the customer with the specified ID
            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound(); // Return NotFound if the customer doesn't exist
            }

            return View(customer); // Return the Delete confirmation view for the customer
        }

        // POST: Customers/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the Customer context is null
            if (_context.Customer == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Customer' is null."); // Return an error if it is
            }

            // Retrieve the customer with the specified ID and include related data
            var customer = await _context.Customer
                .Include(c => c.Vehicles)
                .Include(c => c.Appointments)
                .ThenInclude(a => a.FaultParts)
                .Include(c => c.Faults)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer != null) // If the customer exists
            {
                // Remove related fault parts for each appointment
                foreach (var appointment in customer.Appointments)
                {
                    if (appointment.FaultParts?.Any() == true)
                    {
                        _context.FaultPart.RemoveRange(appointment.FaultParts);
                    }
                }

                // Remove related faults if they exist
                if (customer.Faults?.Any() == true)
                {
                    _context.Fault.RemoveRange(customer.Faults);
                }

                // Remove related appointments if they exist
                if (customer.Appointments?.Any() == true)
                {
                    _context.Appointment.RemoveRange(customer.Appointments);
                }

                // Remove related vehicles if they exist
                if (customer.Vehicles?.Any() == true)
                {
                    _context.Vehicle.RemoveRange(customer.Vehicles);
                }

                // Remove related payments if they exist
                if (customer.Payments?.Any() == true)
                {
                    _context.Payment.RemoveRange(customer.Payments);
                }

                _context.Customer.Remove(customer); // Remove the customer

                await _context.SaveChangesAsync(); // Save changes to the database
            }

            return RedirectToAction(nameof(Index)); // Redirect to the Index action
        }

        // Check if a customer exists by ID
        private bool CustomerExists(int id)
        {
            return (_context.Customer?.Any(e => e.CustomerId == id)).GetValueOrDefault(); // Return true if the customer exists
        }
    }
}
