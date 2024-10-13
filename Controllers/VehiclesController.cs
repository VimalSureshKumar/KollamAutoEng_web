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
        [Authorize(Roles = "Admin,Employee")] // Restrict access to users with Admin or Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set the sort order for customers based on the provided parameter
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer";

            // Check if the search string has changed; reset the page number if it has
            if (searchString != null)
            {
                pageNumber = 1; // Reset page number to 1 for a new search
            }
            else
            {
                searchString = currentFilter; // Preserve the current filter for pagination
            }

            // Check if the Vehicle context is null
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Vehicle' is null."); // Return an error if it is null
            }

            // Store the current search string in ViewData for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Retrieve vehicles from the context, including related Customer, VehicleBrand, and VehicleModel entities
            var vehicles = from veh in _context.Vehicle
                           .Include(m => m.Customer)
                           .Include(m => m.VehicleBrand)
                           .Include(m => m.VehicleModel)
                           select veh;

            // Filter vehicles based on the search string, checking VIN, registration, brand names, model names, and customer names
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(m =>
                    m.VIN.Contains(searchString) || // Check VIN
                    m.Registration.Contains(searchString) || // Check registration
                    m.VehicleBrand.BrandName.Contains(searchString) || // Check brand name
                    m.VehicleModel.ModelName.Contains(searchString) || // Check model name
                    (m.VehicleBrand.BrandName + " " + m.VehicleModel.ModelName).Contains(searchString) || // Combined search for brand and model
                    m.Customer.FirstName.Contains(searchString) || // Check customer's first name
                    m.Customer.LastName.Contains(searchString) || // Check customer's last name
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString) // Combined search for full customer name
                );
            }

            // Sorting logic based on the selected sort order
            switch (sortOrder)
            {
                case "Customer":
                    vehicles = vehicles.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName); // Ascending order
                    break;
                case "customer_desc":
                    vehicles = vehicles.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName); // Descending order
                    break;
            }

            // Define the number of items per page
            int pageSize = 10;
            // Return the paginated list of vehicles to the view
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
                .Include(m => m.Customer)
                .Include(m => m.VehicleBrand)
                .Include(m => m.VehicleModel)
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
                .Include(m => m.Customer)
                .Include(m => m.VehicleBrand)
                .Include(m => m.VehicleModel)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Vehicle' is null.");
            }

            var vehicle = await _context.Vehicle
                .Include(m => m.Appointments) 
                .ThenInclude(m => m.FaultParts) 
                .Include(m => m.Faults) 
                .FirstOrDefaultAsync(m => m.VehicleId == id);

            if (vehicle != null)
            {
                foreach (var appointment in vehicle.Appointments)
                {
                    if (appointment.FaultParts?.Any() == true)
                    {
                        _context.FaultPart.RemoveRange(appointment.FaultParts);
                    }
                }

                if (vehicle.Appointments?.Any() == true)
                {
                    _context.Appointment.RemoveRange(vehicle.Appointments);
                }

                if (vehicle.Faults?.Any() == true)
                {
                    _context.Fault.RemoveRange(vehicle.Faults);
                }

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
