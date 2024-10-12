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
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["BrandNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (_context.VehicleBrand == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var brands = from bra in _context.VehicleBrand
                            select bra;

            if (!String.IsNullOrEmpty(searchString))
            {
                brands = brands.Where(m =>
                    m.BrandName.Contains(searchString) 
                       );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    brands = brands.OrderByDescending(m => m.BrandName);
                    break;
                default:
                    brands = brands.OrderBy(m => m.BrandName);
                    break;
            }

            var brandsList = await brands.ToListAsync();
            int pageSize = 10;
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
