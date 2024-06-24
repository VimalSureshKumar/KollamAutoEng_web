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
    [Authorize(Roles = "Admin")]
    public class AdminControllerFaultpt : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    [Authorize]
    public class FaultPartsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public FaultPartsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: FaultParts
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (_context.FaultPart == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.FaultPart' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var faultparts = from faultp in _context.FaultPart
                               .Include(m => m.Fault)
                               .Include(m => m.Part)
                               .Include(m => m.Appointment)
                             select faultp;

            if (!String.IsNullOrEmpty(searchString))
            {
                faultparts = faultparts.Where(m =>
                    m.Fault.FaultId.ToString().Contains(searchString) ||
                    m.Part.PartId.ToString().Contains(searchString) ||
                    m.Appointment.AppointmentId.ToString().Contains(searchString)
                );
            }

            int pageSize = 5;
            return View(await PaginatedList<FaultPart>.CreateAsync(faultparts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: FaultParts/Details/5
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
                .FirstOrDefaultAsync(m => m.FaultPartId == id);
            if (faultPart == null)
            {
                return NotFound();
            }

            return View(faultPart);
        }

        // GET: FaultParts/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId");
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName");
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "Name");
            return View();
        }

        // POST: FaultParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultPartId,FaultId,PartId,AppointmentId")] FaultPart faultPart)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(faultPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "Name", faultPart.PartId);
            return View(faultPart);
        }

        // GET: FaultParts/Edit/5
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "Name", faultPart.PartId);
            return View(faultPart);
        }

        // POST: FaultParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultPartId,FaultId,PartId,AppointmentId")] FaultPart faultPart)
        {
            if (id != faultPart.FaultPartId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentId", faultPart.AppointmentId);
            ViewData["FaultId"] = new SelectList(_context.Fault, "FaultId", "FaultName", faultPart.FaultId);
            ViewData["PartId"] = new SelectList(_context.Part, "PartId", "Name", faultPart.PartId);
            return View(faultPart);
        }

        // GET: FaultParts/Delete/5
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
                .FirstOrDefaultAsync(m => m.FaultPartId == id);
            if (faultPart == null)
            {
                return NotFound();
            }

            return View(faultPart);
        }

        // POST: FaultParts/Delete/5
        [HttpPost, ActionName("Delete")]
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
