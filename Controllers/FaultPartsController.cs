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
using KollamAutoEng_web.Migrations;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace KollamAutoEng_web.Controllers
{
    // Ensure only users with Admin or Employee roles can access this controller
    [Authorize(Roles = "Admin,Employee")]
    public class FaultPartsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        // Constructor to inject the database context
        public FaultPartsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: FaultParts
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
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

            // Check if the FaultPart context is null
            if (_context.FaultPart == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.FaultPart' is null."); // Return an error if it is
            }

            // Store the current search string for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Query to retrieve fault parts, including related entities
            var faultparts = from faultp in _context.FaultPart
                             .Include(m => m.Fault) // Include related Fault entity
                             .Include(m => m.Part) // Include related Part entity
                             .Include(m => m.Appointment) // Include related Appointment entity
                             .Include(m => m.Customer) // Include related Customer entity
                             .Include(m => m.Vehicle) // Include related Vehicle entity
                             select faultp;

            // If the search string is not empty, filter the fault parts based on various fields
            if (!String.IsNullOrEmpty(searchString))
            {
                faultparts = faultparts.Where(m =>
                    m.Fault.FaultName.Contains(searchString) || // Search by fault name
                    m.Part.PartName.Contains(searchString) || // Search by part name
                    m.Appointment.AppointmentName.Contains(searchString) || // Search by appointment name
                    m.Vehicle.Registration.Contains(searchString) || // Search by vehicle registration
                    m.Customer.FirstName.Contains(searchString) || // Search by customer's first name
                    m.Customer.LastName.Contains(searchString) || // Search by customer's last name
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString) // Search by full customer name
                );
            }

            // Sort fault parts based on the selected sort order
            switch (sortOrder)
            {
                case "Customer":
                    faultparts = faultparts.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName); // Ascending order
                    break;
                case "customer_desc":
                    faultparts = faultparts.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName); // Descending order
                    break;
            }

            int pageSize = 5; // Define the number of items per page
            // Return the paginated list of fault parts to the view
            return View(await PaginatedList<FaultPart>.CreateAsync(faultparts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: FaultParts/Details
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or if the FaultPart context is null
            if (id == null || _context.FaultPart == null)
            {
                return NotFound(); // Return not found if id is null
            }

            // Fetch the fault part details including related entities
            var faultPart = await _context.FaultPart
                .Include(f => f.Appointment)
                .Include(f => f.Fault)
                .Include(f => f.Part)
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultPartId == id);
            if (faultPart == null)
            {
                return NotFound(); // Return not found if the fault part is not found
            }

            return View(faultPart); // Return the details view
        }

        // GET: FaultParts/Create
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public IActionResult Create()
        {
            // Populate dropdown lists for creating a new FaultPart
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName");
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName");
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View(); // Return the create view
        }

        // POST: FaultParts/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        [ValidateAntiForgeryToken] // Validate the request
        public async Task<IActionResult> Create([Bind("FaultPartId,FaultId,PartId,AppointmentId,CustomerId,VehicleId")] FaultPart faultPart)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                // Check for an existing FaultPart with the same FaultId and PartId
                var existingFaultPart = await _context.FaultPart
                    .FirstOrDefaultAsync(fp =>
                        fp.FaultId == faultPart.FaultId &&
                        fp.PartId == faultPart.PartId &&
                        fp.AppointmentId == faultPart.AppointmentId &&
                        fp.CustomerId == faultPart.CustomerId &&
                        fp.VehicleId == faultPart.VehicleId);

                if (existingFaultPart != null) // If a duplicate is found
                {
                    // Add an error message to the model state
                    ModelState.AddModelError("PartId", "This part for the selected fault already exists.");
                }
                else
                {
                    _context.Add(faultPart); // Add the new fault part to the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the index action
                }
            }

            // Repopulate dropdown lists if the model state is invalid or if a duplicate was found
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName", faultPart.PartId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", faultPart.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", faultPart.VehicleId);
            return View(faultPart); // Return the view with validation errors
        }

        // GET: FaultParts/Edit
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null or if the FaultPart context is null
            if (id == null || _context.FaultPart == null)
            {
                return NotFound(); // Return not found if id is null
            }

            // Fetch the fault part for editing
            var faultPart = await _context.FaultPart.FindAsync(id);
            if (faultPart == null)
            {
                return NotFound(); // Return not found if the fault part is not found
            }
            // Populate dropdown lists for editing the FaultPart
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName", faultPart.PartId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", faultPart.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", faultPart.VehicleId);
            return View(faultPart); // Return the edit view
        }

        // POST: FaultParts/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        [ValidateAntiForgeryToken] // Validate the request
        public async Task<IActionResult> Edit(int id, [Bind("FaultPartId,FaultId,PartId,AppointmentId,CustomerId,VehicleId")] FaultPart faultPart)
        {
            if (id != faultPart.FaultPartId) // Check if the id matches the FaultPartId
            {
                return NotFound(); // Return not found if they don't match
            }

            if (ModelState.IsValid) // Check if the model state is valid
            {
                try
                {
                    _context.Update(faultPart); // Update the fault part in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handle concurrency issues
                {
                    if (!FaultPartExists(faultPart.FaultPartId)) // Check if the fault part still exists
                    {
                        return NotFound(); // Return not found if it does not exist
                    }
                    else
                    {
                        throw; // Rethrow the exception if it still exists
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index action after editing
            }
            // Repopulate dropdown lists if the model state is invalid
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName", faultPart.PartId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", faultPart.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", faultPart.VehicleId);
            return View(faultPart); // Return the view with validation errors
        }

        // GET: FaultParts/Delete
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or if the FaultPart context is null
            if (id == null || _context.FaultPart == null)
            {
                return NotFound(); // Return not found if id is null
            }

            // Fetch the fault part to confirm deletion
            var faultPart = await _context.FaultPart
                .Include(f => f.Appointment)
                .Include(f => f.Fault)
                .Include(f => f.Part)
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultPartId == id);
            if (faultPart == null)
            {
                return NotFound(); // Return not found if the fault part is not found
            }

            return View(faultPart); // Return the delete confirmation view
        }

        // POST: FaultParts/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        [ValidateAntiForgeryToken] // Validate the request
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FaultPart == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.FaultPart' is null."); // Return error if the context is null
            }
            // Fetch the fault part to delete
            var faultPart = await _context.FaultPart.FindAsync(id);
            if (faultPart != null) // If the fault part exists
            {
                _context.FaultPart.Remove(faultPart); // Remove it from the context
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index action after deletion
        }

        // Helper method to check if a FaultPart exists
        private bool FaultPartExists(int id)
        {
            return (_context.FaultPart?.Any(e => e.FaultPartId == id)).GetValueOrDefault(); // Check existence in the context
        }
    }
}
