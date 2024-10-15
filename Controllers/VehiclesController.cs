using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Areas.Identity.Data;
using KollamAutoEng_web.Models;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;

namespace KollamAutoEng_web.Controllers
{
    [Authorize(Roles = "Admin,Employee,User")] // Allow access to Admin, Employee, and User roles
    public class VehiclesController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context for the application

        public VehiclesController(KollamAutoEng_webContext context)
        {
            _context = context; // Initialize the context through dependency injection
        }

        // GET: Vehicles
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer"; // Determine sort order for customers

            if (searchString != null) // If a new search is initiated, reset the page number
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter; // Maintain the current filter during pagination
            }

            if (_context.Vehicle == null) // Check if the Vehicle set is null
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Vehicle' is null."); // Return an error if it is null
            }

            ViewData["CurrentFilter"] = searchString; // Store the search string in ViewData for use in the view

            // Query to get all vehicles with related data (Customer, VehicleBrand, VehicleModel)
            var vehicles = from veh in _context.Vehicle
                                .Include(m => m.Customer)
                                .Include(m => m.VehicleBrand)
                                .Include(m => m.VehicleModel)
                           select veh;

            // Filter vehicles based on the search string
            if (!String.IsNullOrEmpty(searchString)) // If a search string is provided
            {
                vehicles = vehicles.Where(m =>
                    m.VIN.Contains(searchString) || // Match VIN
                    m.Registration.Contains(searchString) || // Match Registration
                    m.VehicleBrand.BrandName.Contains(searchString) || // Match Brand Name
                    m.VehicleModel.ModelName.Contains(searchString) || // Match Model Name
                    (m.VehicleBrand.BrandName + " " + m.VehicleModel.ModelName).Contains(searchString) || // Match combined Brand and Model
                    m.Customer.FirstName.Contains(searchString) || // Match Customer's First Name
                    m.Customer.LastName.Contains(searchString) || // Match Customer's Last Name
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString) // Match combined Customer's Full Name
                );
            }

            // Sort the vehicles based on selected sort order
            switch (sortOrder)
            {
                case "Customer":
                    vehicles = vehicles.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName); // Sort ascending by Customer name
                    break;
                case "customer_desc":
                    vehicles = vehicles.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName); // Sort descending by Customer name
                    break;
            }

            int pageSize = 10; // Number of records per page
            return View(await PaginatedList<Vehicle>.CreateAsync(vehicles.AsNoTracking(), pageNumber ?? 1, pageSize)); // Return paginated list of vehicles
        }

        // GET: Vehicles/Details
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null) // Check if id or context is null
            {
                return NotFound(); // Return NotFound if either is null
            }

            // Fetch the vehicle by id including related data (Customer, VehicleBrand, VehicleModel)
            var vehicle = await _context.Vehicle
                .Include(v => v.Customer)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null) // If no vehicle found, return NotFound
            {
                return NotFound();
            }

            return View(vehicle); // Pass the vehicle data to the view
        }

        // GET: Vehicles/Create
        [Authorize(Roles = "Admin,Employee,User")] // Allow access to Admin, Employee, and User roles for creating vehicles
        public IActionResult Create()
        {
            // Populate the dropdown lists for Customer, Brand, and Model
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName");
            return View(); // Return the Create view
        }

        // POST: Vehicles/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee,User")] // Allow access to Admin, Employee, and User roles for creating vehicles
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> Create([Bind("VehicleId,BrandId,ModelId,VIN,Registration,Colour,DriveType,Odometer,CustomerId")] Vehicle vehicle)
        {
            if (ModelState.IsValid) // Check if the submitted model is valid
            {
                _context.Add(vehicle); // Add the new vehicle to the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction("Formsubmit", "Home", new { vehicleId = vehicle.VehicleId }); // Redirect to the form submit page
            }
            // Repopulate dropdown lists if the model state is invalid
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle); // Return the view with validation errors
        }

        // GET: Vehicles/Edit
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles for editing vehicles
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null) // Check if id or context is null
            {
                return NotFound(); // Return NotFound if either is null
            }

            var vehicle = await _context.Vehicle.FindAsync(id); // Find the vehicle by id
            if (vehicle == null) // If no vehicle found, return NotFound
            {
                return NotFound();
            }
            // Populate the dropdown lists for Customer, Brand, and Model
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle); // Pass the vehicle data to the view for editing
        }

        // POST: Vehicles/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles for editing vehicles
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,BrandId,ModelId,VIN,Registration,Colour,DriveType,Odometer,CustomerId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId) // Check if the provided id matches the vehicle id
            {
                return NotFound(); // Return NotFound if ids do not match
            }

            if (ModelState.IsValid) // Check if the submitted model is valid
            {
                try
                {
                    _context.Update(vehicle); // Update the vehicle in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handle concurrency exceptions
                {
                    if (!VehicleExists(vehicle.VehicleId)) // Check if the vehicle still exists
                    {
                        return NotFound(); // Return NotFound if vehicle no longer exists
                    }
                    else
                    {
                        throw; // Rethrow any other exceptions
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the vehicle index page after successful update
            }
            // Repopulate dropdown lists if the model state is invalid
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle); // Return the view with validation errors
        }

        // GET: Vehicles/Delete
        [Authorize(Roles = "Admin")] // Restrict access to Admin role for deleting vehicles
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null) // Check if id or context is null
            {
                return NotFound(); // Return NotFound if either is null
            }

            // Fetch the vehicle by id including related data (Customer, VehicleBrand, VehicleModel)
            var vehicle = await _context.Vehicle
                .Include(v => v.Customer)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null) // If no vehicle found, return NotFound
            {
                return NotFound();
            }

            return View(vehicle); // Pass the vehicle data to the view for confirmation
        }

        // POST: Vehicles/Delete
        [HttpPost, ActionName("Delete")] // Allow POST requests for deletion confirmation
        [Authorize(Roles = "Admin")] // Restrict access to Admin role for deleting vehicles
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null) // Check if the Vehicle set is null
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Vehicle' is null."); // Return an error if it is null
            }

            // Fetch the vehicle by id including related data (Appointments and Faults)
            var vehicle = await _context.Vehicle
                .Include(v => v.Appointments) // Include related Appointments
                    .ThenInclude(a => a.FaultParts) // Include related FaultParts for each Appointment
                .Include(v => v.Faults) // Include related Faults
                .FirstOrDefaultAsync(v => v.VehicleId == id);

            if (vehicle != null) // If vehicle found
            {
                try
                {
                    // Remove related Faults
                    if (vehicle.Faults?.Any() == true) // Check if there are related Faults
                    {
                        _context.Fault.RemoveRange(vehicle.Faults); // Remove related Faults
                    }

                    // Remove related FaultParts for each Appointment
                    foreach (var appointment in vehicle.Appointments)
                    {
                        if (appointment.FaultParts?.Any() == true) // Check if there are related FaultParts
                        {
                            _context.FaultPart.RemoveRange(appointment.FaultParts); // Remove related FaultParts
                        }
                    }

                    // Remove related Appointments
                    if (vehicle.Appointments?.Any() == true) // Check if there are related Appointments
                    {
                        _context.Appointment.RemoveRange(vehicle.Appointments); // Remove related Appointments
                    }

                    // Finally, remove the Vehicle
                    _context.Vehicle.Remove(vehicle); // Remove the Vehicle from the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (Exception ex) // Handle any exceptions that occur during deletion
                {
                    return Problem($"An error occurred while deleting: {ex.Message}"); // Return an error message
                }
            }

            return RedirectToAction(nameof(Index)); // Redirect to the vehicle index page after deletion
        }

        private bool VehicleExists(int id) // Check if a vehicle exists by id
        {
            return (_context.Vehicle?.Any(e => e.VehicleId == id)).GetValueOrDefault(); // Return true if exists, false otherwise
        }
    }
}
