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
    public class PartsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public PartsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Parts
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CostParm"] = sortOrder == "Cost" ? "cost_desc" : "Cost";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (_context.Part == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Part' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var parts = from par in _context.Part
                            select par;

            if (!String.IsNullOrEmpty(searchString))
            {
                parts = parts.Where(m =>
                    m.Reference.Contains(searchString) ||
                    m.PartName.Contains(searchString) ||
                    m.Cost.ToString().Contains(searchString) 
                );
            }

            switch (sortOrder)
            {
                case "Cost":
                    parts = parts.OrderBy(c => c.Cost);
                    break;
                case "cost_desc":
                    parts = parts.OrderByDescending(c => c.Cost);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Part>.CreateAsync(parts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Parts/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Part == null)
            {
                return NotFound();
            }

            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // GET: Parts/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parts/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartId,Reference,PartName,Cost")] Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(part);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        // GET: Parts/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Part == null)
            {
                return NotFound();
            }

            var part = await _context.Part.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        // POST: Parts/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartId,Reference,PartName,Cost")] Part part)
        {
            if (id != part.PartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.PartId))
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
            return View(part);
        }

        // GET: Parts/Delete
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Part == null)
            {
                return NotFound();
            }

            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // POST: Parts/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Part == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Part'  is null.");
            }
            var part = await _context.Part.FindAsync(id);
            if (part != null)
            {
                _context.Part.Remove(part);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartExists(int id)
        {
          return (_context.Part?.Any(e => e.PartId == id)).GetValueOrDefault();
        }
    }
}
