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
    // Restrict access to users with "Admin" or "Employee" roles
    [Authorize(Roles = "Admin,Employee")]
    public class VehicleModelsController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context for accessing data

        // Constructor to initialize the context
        public VehicleModelsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: VehicleModels
        // Method to display the list of vehicle models with sorting and searching features
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder; // Save current sort order
            ViewData["BrandNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // Set sort parameter

            // Reset page number if the search string is changed
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter; // Maintain current filter if not searching
            }

            // Check if the VehicleModel entity set is null
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleModel' is null.");
            }

            ViewData["CurrentFilter"] = searchString; // Store current filter value

            // Query vehicle models including their associated brands
            var models = from mod in _context.VehicleModel.Include(m => m.VehicleBrand)
                         select mod;

            // Apply search filter if provided
            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(m =>
                    m.ModelName.Contains(searchString) ||
                    m.VehicleBrand.BrandName.Contains(searchString) ||
                    (m.VehicleBrand.BrandName + " " + m.ModelName).Contains(searchString) ||
                    (m.ModelName + " " + m.VehicleBrand.BrandName).Contains(searchString));
            }

            // Apply sorting based on the sort order
            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.VehicleBrand.BrandName); // Sort by brand name descending
                    break;
                default:
                    models = models.OrderBy(m => m.VehicleBrand.BrandName); // Sort by brand name ascending
                    break;
            }

            // Execute the query and convert the result to a list
            var modelsList = await models.ToListAsync();

            int pageSize = 10; // Set the page size for pagination
            // Return the view with paginated list of vehicle models
            return View(await PaginatedList<VehicleModel>.CreateAsync(models.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: VehicleModels/Details
        // Method to display the details of a specific vehicle model
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or the VehicleModel entity set is null
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound(); // Return NotFound if no id is provided
            }

            // Fetch the vehicle model along with its associated brand
            var vehicleModel = await _context.VehicleModel
                .Include(v => v.VehicleBrand)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (vehicleModel == null)
            {
                return NotFound(); // Return NotFound if the vehicle model doesn't exist
            }

            return View(vehicleModel); // Return the view with the vehicle model details
        }

        // GET: VehicleModels/Create
        // Method to display the view for creating a new vehicle model
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            // Populate the select list for vehicle brands
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
            return View(); // Return the view for creating a vehicle model
        }

        // POST: VehicleModels/Create
        // Method to handle the creation of a new vehicle model
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost] // Specify that this action is for POST requests
        [ValidateAntiForgeryToken] // Validate anti-forgery token
        public async Task<IActionResult> Create([Bind("ModelId,ModelName,BrandId")] VehicleModel vehicleModel)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                _context.Add(vehicleModel); // Add the new vehicle model to the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index action
            }
            // Repopulate the select list in case of validation failure
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);
            return View(vehicleModel); // Return the view with the current vehicle model
        }

        // GET: VehicleModels/Edit
        // Method to display the view for editing a specific vehicle model
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null or the VehicleModel entity set is null
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound(); // Return NotFound if no id is provided
            }

            // Fetch the vehicle model by its id
            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound(); // Return NotFound if the vehicle model doesn't exist
            }
            // Populate the select list for vehicle brands
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);
            return View(vehicleModel); // Return the view for editing the vehicle model
        }

        // POST: VehicleModels/Edit
        // Method to handle the update of an existing vehicle model
        [HttpPost] // Specify that this action is for POST requests
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken] // Validate anti-forgery token
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,ModelName,BrandId")] VehicleModel vehicleModel)
        {
            // Check if the id matches the model's id
            if (id != vehicleModel.ModelId)
            {
                return NotFound(); // Return NotFound if the ids do not match
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleModel); // Update the vehicle model in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issues if the vehicle model no longer exists
                    if (!VehicleModelExists(vehicleModel.ModelId))
                    {
                        return NotFound(); // Return NotFound if the vehicle model doesn't exist
                    }
                    else
                    {
                        throw; // Re-throw the exception for handling elsewhere
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index action
            }
            // Repopulate the select list in case of validation failure
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);
            return View(vehicleModel); // Return the view with the current vehicle model
        }

        // GET: VehicleModels/Delete
        // Method to display the view for confirming the deletion of a vehicle model
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or the VehicleModel entity set is null
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound(); // Return NotFound if no id is provided
            }

            // Fetch the vehicle model along with its associated brand
            var vehicleModel = await _context.VehicleModel
                .Include(v => v.VehicleBrand)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (vehicleModel == null)
            {
                return NotFound(); // Return NotFound if the vehicle model doesn't exist
            }

            return View(vehicleModel); // Return the view for confirming deletion
        }

        // POST: VehicleModels/Delete
        // Method to handle the actual deletion of a vehicle model
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken] // Validate anti-forgery token
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the VehicleModel entity set is null
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleModel' is null.");
            }
            // Fetch the vehicle model by its id
            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel != null)
            {
                _context.VehicleModel.Remove(vehicleModel); // Remove the vehicle model from the context
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index action
        }

        // Helper method to check if a vehicle model exists by its id
        private bool VehicleModelExists(int id)
        {
            return (_context.VehicleModel?.Any(e => e.ModelId == id)).GetValueOrDefault(); // Check for existence
        }
    }
}
