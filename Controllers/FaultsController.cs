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
    [Authorize(Roles = "Admin,Employee")]
    public class FaultsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

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
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // GET: Faults/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View();
        }

        // POST: Faults/Create
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultId,VehicleId,CustomerId,FaultName")] Fault fault)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault);
        }

        // GET: Faults/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault.FindAsync(id);
            if (fault == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault);
        }

        // POST: Faults/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultId,VehicleId,CustomerId,FaultName")] Fault fault)
        {
            if (id != fault.FaultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultExists(fault.FaultId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault);
        }

        // GET: Faults/Delete
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // POST: Faults/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fault == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Fault'  is null.");
            }
            var fault = await _context.Fault.FindAsync(id);
            if (fault != null)
            {
                _context.Fault.Remove(fault);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultExists(int id)
        {
            return (_context.Fault?.Any(e => e.FaultId == id)).GetValueOrDefault();
        }
    }
}