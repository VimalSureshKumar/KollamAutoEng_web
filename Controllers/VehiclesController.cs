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
    public class VehiclesController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehiclesController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var kollamAutoEng_webContext = _context.Vehicle.Include(v => v.Customer).Include(v => v.VehicleBrand).Include(v => v.VehicleModel);
            return View(await kollamAutoEng_webContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Customer)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,BrandId,ModelId,VIN,Registration,Odometer,DriveType,CustomerId")] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,BrandId,ModelId,VIN,Registration,Odometer,DriveType,CustomerId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Customer)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return (_context.Vehicle?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
