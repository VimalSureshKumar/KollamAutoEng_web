using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Areas.Identity.Data;
using KollamAutoEng_web.Models;

namespace KollamAutoEng_web.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehicleModelsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: VehicleModels
        public async Task<IActionResult> Index()
        {
            var kollamAutoEng_webContext = _context.VehicleModel.Include(v => v.VehicleBrand);
            return View(await kollamAutoEng_webContext.ToListAsync());
        }

        // GET: VehicleModels/Details/5
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
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,ModelName,BrandId")] VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(vehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicleModel.BrandId);
            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
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

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,ModelName,BrandId")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.ModelId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
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

        // GET: VehicleModels/Delete/5
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

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleModel'  is null.");
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
