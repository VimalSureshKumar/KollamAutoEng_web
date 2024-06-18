﻿using System;
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
    public class VehicleBrandsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public VehicleBrandsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: VehicleBrands
        public async Task<IActionResult> Index()
        {
              return _context.VehicleBrand != null ? 
                          View(await _context.VehicleBrand.ToListAsync()) :
                          Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand'  is null.");
        }

        // GET: VehicleBrands/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,BrandName")] VehicleBrand vehicleBrand)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(vehicleBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleBrand);
        }

        // GET: VehicleBrands/Edit/5
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName")] VehicleBrand vehicleBrand)
        {
            if (id != vehicleBrand.BrandId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
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

        // GET: VehicleBrands/Delete/5
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

        // POST: VehicleBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleBrand == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.VehicleBrand'  is null.");
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