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
using KollamAutoEng_web.Migrations;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace KollamAutoEng_web.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class FaultPartsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public FaultPartsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: FaultParts
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
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

            if (_context.FaultPart == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.FaultPart' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var faultparts = from faultp in _context.FaultPart
                               .Include(m => m.Fault)
                               .Include(m => m.Part)
                               .Include(m => m.Appointment)
                               .Include(m => m.Customer)
                               .Include(m => m.Vehicle)
                             select faultp;

            if (!String.IsNullOrEmpty(searchString))
            {
                faultparts = faultparts.Where(m =>
                    m.Fault.FaultName.Contains(searchString) ||
                    m.Part.PartName.Contains(searchString) ||
                    m.Appointment.AppointmentName.Contains(searchString) ||
                    m.Vehicle.Registration.Contains(searchString) ||
                    m.Customer.FirstName.Contains(searchString) ||
                    m.Customer.LastName.Contains(searchString) ||
                   (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString)
                );
            }

            switch (sortOrder)
            {
                case "Customer":
                    faultparts = faultparts.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName);
                    break;
                case "customer_desc":
                    faultparts = faultparts.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<FaultPart>.CreateAsync(faultparts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: FaultParts/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FaultPart == null)
            {
                return NotFound();
            }

            var faultPart = await _context.FaultPart
                .Include(f => f.Appointment)
                .Include(f => f.Fault)
                .Include(f => f.Part)
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultPartId == id);
            if (faultPart == null)
            {
                return NotFound();
            }

            return View(faultPart);
        }

        // GET: FaultParts/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName");
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName");
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View();
        }

        // POST: FaultParts/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultPartId,FaultId,PartId,AppointmentId,CustomerId,VehicleId")] FaultPart faultPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faultPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName", faultPart.PartId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", faultPart.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", faultPart.VehicleId);
            return View(faultPart);
        }

        // GET: FaultParts/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FaultPart == null)
            {
                return NotFound();
            }

            var faultPart = await _context.FaultPart.FindAsync(id);
            if (faultPart == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName", faultPart.PartId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", faultPart.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", faultPart.VehicleId);
            return View(faultPart);
        }

        // POST: FaultParts/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultPartId,FaultId,PartId,AppointmentId,CustomerId,VehicleId")] FaultPart faultPart)
        {
            if (id != faultPart.FaultPartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faultPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultPartExists(faultPart.FaultPartId))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentName", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "PartName", faultPart.PartId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", faultPart.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", faultPart.VehicleId);
            return View(faultPart);
        }

        // GET: FaultParts/Delete
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FaultPart == null)
            {
                return NotFound();
            }

            var faultPart = await _context.FaultPart
                .Include(f => f.Appointment)
                .Include(f => f.Fault)
                .Include(f => f.Part)
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultPartId == id);
            if (faultPart == null)
            {
                return NotFound();
            }

            return View(faultPart);
        }

        // POST: FaultParts/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FaultPart == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.FaultPart'  is null.");
            }
            var faultPart = await _context.FaultPart.FindAsync(id);
            if (faultPart != null)
            {
                _context.FaultPart.Remove(faultPart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultPartExists(int id)
        {
          return (_context.FaultPart?.Any(e => e.FaultPartId == id)).GetValueOrDefault();
        }
    }
}
