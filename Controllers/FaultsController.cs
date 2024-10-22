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
    [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
    public class FaultsController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context

        // Constructor to inject the database context
        public FaultsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Faults
        [Authorize(Roles = "Admin,Employee")] // Ensures only users with Admin or Employee roles can access this action
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set up sorting parameters for customer sorting
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer";

            // Reset the page number if a new search is performed
            if (searchString != null)
            {
                pageNumber = 1; // Reset to the first page when a new search is made
            }
            else
            {
                // Retain the current filter string for pagination
                searchString = currentFilter;
            }

            // Check if the Fault context is null
            if (_context.Fault == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Fault' is null."); // Return an error if it is
            }

            // Store the current search string for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Query to retrieve faults, including related entities
            var faults = from fau in _context.Fault
                         .Include(m => m.Vehicle) // Eager load Vehicle
                         .Include(m => m.Customer) // Eager load Customer
                         select fau;

            // If the search string is not empty, filter the faults based on various fields
            if (!String.IsNullOrEmpty(searchString))
            {
                faults = faults.Where(m =>
                    m.Vehicle.Registration.Contains(searchString) || // Search by vehicle registration
                    m.FaultName.Contains(searchString) || // Search by fault name
                    m.Customer.FirstName.Contains(searchString) || // Search by customer's first name
                    m.Customer.LastName.Contains(searchString) || // Search by customer's last name
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString) // Search by full customer name
                );
            }

            // Sort faults based on the selected sort order
            switch (sortOrder)
            {
                case "Customer":
                    faults = faults.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName); // Ascending order
                    break;
                case "customer_desc":
                    faults = faults.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName); // Descending order
                    break;
            }

            int pageSize = 10; // Define the number of items per page
            // Return the paginated list of faults to the view
            return View(await PaginatedList<Fault>.CreateAsync(faults.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Faults/Details
        [Authorize(Roles = "Admin,Employee")] // Ensure only users with appropriate roles can access
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fault == null) // Check if ID is null or Fault context is unavailable
            {
                return NotFound(); // Return Not Found if ID is null
            }

            // Retrieve the fault details along with related customer and vehicle information
            var fault = await _context.Fault
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound(); // Return Not Found if no fault matches the ID
            }

            return View(fault); // Return the fault details view
        }

        // GET: Faults/Create
        [Authorize(Roles = "Admin,Employee")] // Ensure only authorized users can access this action
        public IActionResult Create()
        {
            // Populate dropdown lists for selecting customer and vehicle
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View(); // Return the view to create a new fault
        }

        // POST: Faults/Create
        [Authorize(Roles = "Admin,Employee")] // Ensure only authorized users can access this action
        [HttpPost] // Specifies that this action responds to POST requests
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> Create([Bind("FaultId,VehicleId,CustomerId,FaultName")] Fault fault)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                // Check for an existing fault with the same VehicleId and FaultName
                var existingFault = await _context.Fault
                    .FirstOrDefaultAsync(f => f.VehicleId == fault.VehicleId && f.FaultName == fault.FaultName);

                if (existingFault != null) // If a duplicate is found
                {
                    // Add an error message to the model state
                    ModelState.AddModelError("FaultName", "A fault with this name for the selected vehicle already exists.");
                }
                else
                {
                    _context.Add(fault); // Add the new fault to the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the index action
                }
            }

            // Repopulate dropdown lists if the model state is invalid or if a duplicate was found
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault); // Return the view with validation errors
        }

        // GET: Faults/Edit
        [Authorize(Roles = "Admin,Employee")] // Ensure only authorized users can access this action
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fault == null) // Check if ID is null or Fault context is unavailable
            {
                return NotFound(); // Return Not Found if ID is null
            }

            var fault = await _context.Fault.FindAsync(id); // Retrieve fault by ID
            if (fault == null) // Check if fault exists
            {
                return NotFound(); // Return Not Found if no fault matches the ID
            }
            // Populate dropdown lists for editing the fault
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault); // Return the view to edit the fault
        }

        // POST: Faults/Edit
        [HttpPost] // Specifies that this action responds to POST requests
        [Authorize(Roles = "Admin,Employee")] // Ensure only authorized users can access this action
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> Edit(int id, [Bind("FaultId,VehicleId,CustomerId,FaultName")] Fault fault)
        {
            if (id != fault.FaultId) // Ensure the ID in the route matches the fault ID
            {
                return NotFound(); // Return Not Found if they do not match
            }

            if (ModelState.IsValid) // Check if the model state is valid
            {
                try
                {
                    _context.Update(fault); // Update the fault in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handle concurrency issues
                {
                    if (!FaultExists(fault.FaultId)) // Check if the fault still exists
                    {
                        return NotFound(); // Return Not Found if no fault matches the ID
                    }
                    else
                    {
                        throw; // Rethrow the exception if it's not a concurrency issue
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index action
            }
            // Repopulate dropdown lists if the model state is invalid
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault); // Return the view with validation errors
        }

        // GET: Faults/Delete
        [Authorize(Roles = "Admin,Employee")] // Ensure only authorized users can access this action
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fault == null) // Check if ID is null or Fault context is unavailable
            {
                return NotFound(); // Return Not Found if ID is null
            }

            // Retrieve the fault details along with related customer and vehicle information
            var fault = await _context.Fault
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null) // Check if fault exists
            {
                return NotFound(); // Return Not Found if no fault matches the ID
            }

            return View(fault); // Return the view to confirm deletion
        }

        // POST: Faults/Delete
        [HttpPost, ActionName("Delete")] // Specifies this action is called "Delete" for POST requests
        [Authorize(Roles = "Admin,Employee")] // Ensure only authorized users can access this action
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fault == null) // Check if Fault context is unavailable
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Fault'  is null."); // Return an error
            }
            var fault = await _context.Fault.FindAsync(id); // Find the fault by ID
            if (fault != null) // If the fault exists
            {
                _context.Fault.Remove(fault); // Remove the fault from the context
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index action
        }

        // Check if a fault exists by ID
        private bool FaultExists(int id)
        {
            return (_context.Fault?.Any(e => e.FaultId == id)).GetValueOrDefault(); // Return true if the fault exists
        }
    }
}
