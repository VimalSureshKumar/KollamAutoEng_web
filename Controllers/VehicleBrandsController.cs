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
    [Authorize(Roles = "Admin,Employee")] // Authorize access to Admin and Employee roles only
    public class VehicleBrandsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehicleBrandsController(KollamAutoEng_webContext context)
        {
            _context = context; // Dependency injection of the database context
        }

        // GET: VehicleBrands
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder; // Store the current sort order in ViewData
            ViewData["BrandNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // Determine sort direction

            if (searchString != null) // If a new search is initiated, reset the page number to 1
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter; // Maintain the current filter during pagination
            }

            if (_context.VehicleBrand == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand' is null."); // Return error if VehicleBrand set is null
            }

            ViewData["CurrentFilter"] = searchString; // Store the search string in ViewData for use in the view

            var brands = from bra in _context.VehicleBrand select bra; // Query to get all vehicle brands

            if (!String.IsNullOrEmpty(searchString)) // If search string is provided, filter the brands by name
            {
                brands = brands.Where(m => m.BrandName.Contains(searchString));
            }

            switch (sortOrder) // Sort based on the selected sort order
            {
                case "name_desc":
                    brands = brands.OrderByDescending(m => m.BrandName); // Sort descending by brand name
                    break;
                default:
                    brands = brands.OrderBy(m => m.BrandName); // Sort ascending by default
                    break;
            }

            int pageSize = 10; // Number of records per page
            return View(await PaginatedList<VehicleBrand>.CreateAsync(brands.AsNoTracking(), pageNumber ?? 1, pageSize)); // Paginated list of brands
        }

        // GET: VehicleBrands/Details
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleBrand == null) // Check if id or context is null
            {
                return NotFound(); // Return NotFound if either is null
            }

            var vehicleBrand = await _context.VehicleBrand
                .FirstOrDefaultAsync(m => m.BrandId == id); // Get the vehicle brand by id

            if (vehicleBrand == null) // If no brand found, return NotFound
            {
                return NotFound();
            }

            return View(vehicleBrand); // Pass the brand data to the view
        }

        // GET: VehicleBrands/Create
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        public IActionResult Create()
        {
            return View(); // Display the Create view for adding a new brand
        }

        // POST: VehicleBrands/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        [ValidateAntiForgeryToken] // Prevent cross-site request forgery (CSRF) attacks
        public async Task<IActionResult> Create([Bind("BrandId,BrandName")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid) // Check if the submitted model is valid
            {
                _context.Add(vehicleBrand); // Add the new brand to the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the brand index page
            }
            return View(vehicleBrand); // Return the view with validation errors if model is invalid
        }

        // GET: VehicleBrands/Edit
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleBrand == null) // Check if id or context is null
            {
                return NotFound(); // Return NotFound if either is null
            }

            var vehicleBrand = await _context.VehicleBrand.FindAsync(id); // Find the brand by id
            if (vehicleBrand == null) // If no brand found, return NotFound
            {
                return NotFound();
            }
            return View(vehicleBrand); // Pass the brand data to the view for editing
        }

        // POST: VehicleBrands/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restrict access to Admin and Employee roles
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName")] VehicleBrand vehicleBrand)
        {
            if (id != vehicleBrand.BrandId) // Check if the provided id matches the brand id
            {
                return NotFound(); // Return NotFound if ids do not match
            }

            if (ModelState.IsValid) // Check if the submitted model is valid
            {
                try
                {
                    _context.Update(vehicleBrand); // Update the brand in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handle concurrency exceptions
                {
                    if (!VehicleBrandExists(vehicleBrand.BrandId)) // Check if the brand still exists
                    {
                        return NotFound(); // Return NotFound if brand no longer exists
                    }
                    else
                    {
                        throw; // Rethrow any other exceptions
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the brand index page after successful update
            }
            return View(vehicleBrand); // Return the view with validation errors if model is invalid
        }

        // GET: VehicleBrands/Delete
        [Authorize(Roles = "Admin")] // Restrict access to Admin role only for deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleBrand == null) // Check if id or context is null
            {
                return NotFound(); // Return NotFound if either is null
            }

            var vehicleBrand = await _context.VehicleBrand
                .FirstOrDefaultAsync(m => m.BrandId == id); // Get the brand by id

            if (vehicleBrand == null) // If no brand found, return NotFound
            {
                return NotFound();
            }

            return View(vehicleBrand); // Pass the brand data to the view for confirmation before deletion
        }

        // POST: VehicleBrands/Delete
        [HttpPost, ActionName("Delete")] // Specify the action name for the form submission
        [Authorize(Roles = "Admin")]  // Restrict access to Admin role only for deletion
        [ValidateAntiForgeryToken] // Prevent CSRF attacks
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleBrand == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand' is null."); // Return an error if context is null
            }

            // Fetch the vehicle brand by id, including related vehicle models, vehicles, and faults
            var vehicleBrand = await _context.VehicleBrand
                .Include(vb => vb.VehicleModels)  // Include related vehicle models
                .ThenInclude(vm => vm.Vehicles)   // Include related vehicles
                .ThenInclude(v => v.Faults)       // Include related faults
                .FirstOrDefaultAsync(vb => vb.BrandId == id);

            if (vehicleBrand != null)
            {
                // Loop through related vehicle models and delete associated faults and vehicles
                if (vehicleBrand.VehicleModels != null && vehicleBrand.VehicleModels.Any())
                {
                    foreach (var vehicleModel in vehicleBrand.VehicleModels)
                    {
                        // Loop through related vehicles and delete associated faults
                        if (vehicleModel.Vehicles != null && vehicleModel.Vehicles.Any())
                        {
                            foreach (var vehicle in vehicleModel.Vehicles)
                            {
                                if (vehicle.Faults != null && vehicle.Faults.Any())
                                {
                                    _context.Fault.RemoveRange(vehicle.Faults); // Remove all faults for this vehicle
                                }

                                _context.Vehicle.Remove(vehicle); // Remove the vehicle itself
                            }
                        }

                        _context.VehicleModel.Remove(vehicleModel); // Remove the vehicle model
                    }
                }

                _context.VehicleBrand.Remove(vehicleBrand); // Remove the vehicle brand itself
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the brand index page after deletion
        }

        private bool VehicleBrandExists(int id)
        {
            // Check if a vehicle brand exists in the context by its id
            return (_context.VehicleBrand?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
