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
    public class VehicleModelsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehicleModelsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: VehicleModels
        [Authorize(Roles = "Admin,Employee")] // Restrict access to users with Admin or Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set the current sort order and initialize the sorting parameter for model names
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // Check if the search string has been modified; reset the page number if it has
            if (searchString != null)
            {
                pageNumber = 1; // Reset page number to 1 if searching
            }
            else
            {
                searchString = currentFilter; // Preserve the current filter for pagination
            }

            // Check if the VehicleModel context is null
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleModel' is null."); // Return an error if it is null
            }

            // Store the current search string in ViewData for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Retrieve vehicle models from the context, including related VehicleBrand entities
            var models = from mod in _context.VehicleModel
                         .Include(m => m.VehicleBrand)
                         select mod;

            // Filter vehicle models based on the search string, checking model names and brand names
            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(m =>
                    m.ModelName.Contains(searchString) || // Check model name
                    m.VehicleBrand.BrandName.Contains(searchString) || // Check associated brand name
                    (m.VehicleBrand.BrandName + " " + m.ModelName).Contains(searchString) || // Combined search
                    (m.ModelName + " " + m.VehicleBrand.BrandName).Contains(searchString) // Combined search
                );
            }

            // Sorting logic based on the selected sort order
            switch (sortOrder)
            {
                case "name_desc":
                    models = models.OrderByDescending(m => m.VehicleBrand.BrandName); // Descending order by brand name
                    break;
                default:
                    models = models.OrderBy(m => m.VehicleBrand.BrandName); // Ascending order by brand name
                    break;
            }

            // Define the number of items per page
            int pageSize = 10;
            // Return the paginated list of vehicle models to the view
            return View(await PaginatedList<VehicleModel>.CreateAsync(models.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: VehicleModels/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel
                .Include(v => v.VehicleBrand)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
            return View();
        }

        // POST: VehicleModels/Create
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,ModelName,BrandId")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);

            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,ModelName,BrandId")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleModelExists(vehicleModel.ModelId))
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
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel
                .Include(v => v.VehicleBrand)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleModel' is null.");  
            }
            var vehicleModel = await _context.VehicleModel.FindAsync(id);  
            if (vehicleModel != null)
            {
                _context.VehicleModel.Remove(vehicleModel);  
            }

            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index)); 
        }

        private bool VehicleModelExists(int id)
        {
          return (_context.VehicleModel?.Any(e => e.ModelId == id)).GetValueOrDefault();
        }
    }
}
