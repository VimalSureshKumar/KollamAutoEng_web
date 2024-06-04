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
    public class FaultsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public FaultsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Faults
        public async Task<IActionResult> Index()
        {
              return _context.Fault != null ? 
                          View(await _context.Fault.ToListAsync()) :
                          Problem("Entity set 'KollamAutoEng_webContext.Fault'  is null.");
        }

        // GET: Faults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // GET: Faults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultId,Description")] Fault fault)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(fault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fault);
        }

        // GET: Faults/Edit/5
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
            return View(fault);
        }

        // POST: Faults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultId,Description")] Fault fault)
        {
            if (id != fault.FaultId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
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
            return View(fault);
        }

        // GET: Faults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fault == null)
            {
                return NotFound();
            }

            var fault = await _context.Fault
                .FirstOrDefaultAsync(m => m.FaultId == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // POST: Faults/Delete/5
        [HttpPost, ActionName("Delete")]
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
