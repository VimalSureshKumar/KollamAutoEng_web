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
    [Authorize(Roles = "Admin,Employee,User")]
    public class VehiclesController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehiclesController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Vehicle' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicles = from veh in _context.Vehicle
                                .Include(m => m.Customer)
                                .Include(m => m.VehicleBrand)
                                .Include(m => m.VehicleModel)
                           select veh;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(m =>
                    m.VIN.Contains(searchString) ||
                    m.Registration.Contains(searchString) ||
                    m.VehicleBrand.BrandName.Contains(searchString) ||
                    m.VehicleModel.ModelName.Contains(searchString) ||
                    (m.VehicleBrand.BrandName + " " + m.VehicleModel.ModelName).Contains(searchString) ||
                    m.Customer.FirstName.Contains(searchString) ||
                    m.Customer.LastName.Contains(searchString) ||
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString)
                );
            }

            switch (sortOrder)
            {
                case "Customer":
                    vehicles = vehicles.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName);
                    break;
                case "customer_desc":
                    vehicles = vehicles.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Vehicle>.CreateAsync(vehicles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Vehicles/Details
        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee,User")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName");
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName");
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee,User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,BrandId,ModelId,VIN,Registration,Colour,DriveType,Odometer,CustomerId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Formsubmit", "Home", new { vehicleId = vehicle.VehicleId });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", vehicle.CustomerId);
            ViewData["BrandId"] = new SelectList(_context.VehicleBrand, "BrandId", "BrandName", vehicle.BrandId);
            ViewData["ModelId"] = new SelectList(_context.VehicleModel, "ModelId", "ModelName", vehicle.ModelId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit
        [Authorize(Roles = "Admin,Employee")]
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

        // POST: Vehicles/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,BrandId,ModelId,VIN,Registration,Colour,DriveType,Odometer,CustomerId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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

        // GET: Vehicles/Delete
        [Authorize(Roles = "Admin,Employee")]
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

        // POST: Vehicles/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
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
