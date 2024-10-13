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
    public class VehicleBrandsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehicleBrandsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: VehicleBrands
        [Authorize(Roles = "Admin,Employee")] // Restrict access to users with Admin or Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set the current sort order and initialize the sorting parameter for brand names
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

            // Check if the VehicleBrand context is null
            if (_context.VehicleBrand == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand' is null."); // Return an error if it is null
            }

            // Store the current search string in ViewData for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Retrieve vehicle brands from the context
            var brands = from bra in _context.VehicleBrand
                         select bra;

            // Filter vehicle brands based on the search string, checking brand names
            if (!String.IsNullOrEmpty(searchString))
            {
                brands = brands.Where(m => m.BrandName.Contains(searchString)); // Filter by brand name
            }

            // Sorting logic based on the selected sort order
            switch (sortOrder)
            {
                case "name_desc":
                    brands = brands.OrderByDescending(m => m.BrandName); // Descending order
                    break;
                default:
                    brands = brands.OrderBy(m => m.BrandName); // Ascending order
                    break;
            }

            // Define the number of items per page
            int pageSize = 10;
            // Return the paginated list of vehicle brands to the view
            return View(await PaginatedList<VehicleBrand>.CreateAsync(brands.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: VehicleBrands/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleBrand == null)
            {
                return NotFound();
            }

            var vehicleBrand = await _context.VehicleBrand
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (vehicleBrand == null)
            {
                return NotFound();
            }

            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleBrands/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName")] VehicleBrand vehicleBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleBrand == null)
            {
                return NotFound();
            }

            var vehicleBrand = await _context.VehicleBrand.FindAsync(id);
            if (vehicleBrand == null)
            {
                return NotFound();
            }
            return View(vehicleBrand);
        }

        // POST: VehicleBrands/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName")] VehicleBrand vehicleBrand)
        {
            if (id != vehicleBrand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleBrandExists(vehicleBrand.BrandId))
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
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleBrand == null)
            {
                return NotFound();
            }

            var vehicleBrand = await _context.VehicleBrand
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (vehicleBrand == null)
            {
                return NotFound();
            }

            return View(vehicleBrand);
        }

        // POST: VehicleBrands/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleBrand == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand' is null."); 
            }
            var vehicleBrand = await _context.VehicleBrand.FindAsync(id);  
            if (vehicleBrand != null)
            {
                _context.VehicleBrand.Remove(vehicleBrand);  
            }

            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleBrandExists(int id)
        {
          return (_context.VehicleBrand?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
