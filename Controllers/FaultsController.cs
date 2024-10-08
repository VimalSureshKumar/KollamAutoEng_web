﻿using System;
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
    public class FaultsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public FaultsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Faults
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (_context.Fault == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Fault' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var faults = from fau in _context.Fault
                               .Include(m => m.Vehicle)
                               .Include(m => m.Customer)
                         select fau;

            if (!String.IsNullOrEmpty(searchString))
            {
                faults = faults.Where(m =>
                   m.Vehicle.Registration.Contains(searchString) ||
                   m.Customer.FirstName.Contains(searchString)
                       );
            }

            int pageSize = 10;
            return View(await PaginatedList<Fault>.CreateAsync(faults.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Faults/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // GET: Faults/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View();
        }

        // POST: Faults/Create
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultId,VehicleId,CustomerId,FaultName")] Fault fault)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault);
        }

        // GET: Faults/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault.FindAsync(id);
            if (fault == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault);
        }

        // POST: Faults/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultId,VehicleId,CustomerId,FaultName")] Fault fault)
        {
            if (id != fault.FaultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultExists(fault.FaultId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", fault.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", fault.VehicleId);
            return View(fault);
        }

        // GET: Faults/Delete
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .Include(f => f.Customer)
                .Include(f => f.Vehicle)
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // POST: Faults/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fault == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Fault'  is null.");
            }
            var fault = await _context.Fault.FindAsync(id);
            if (fault != null)
            {
                _context.Fault.Remove(fault);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultExists(int id)
        {
            return (_context.Fault?.Any(e => e.FaultId == id)).GetValueOrDefault();
        }
    }
}