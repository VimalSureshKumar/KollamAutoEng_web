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
    public class SupervisorsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public SupervisorsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Supervisors
        public async Task<IActionResult> Index()
        {
              return _context.Supervisor != null ? 
                          View(await _context.Supervisor.ToListAsync()) :
                          Problem("Entity set 'KollamAutoEng_webContext.Supervisor'  is null.");
        }

        // GET: Supervisors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Supervisor == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor
                .FirstOrDefaultAsync(m => m.SupervisorId == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // GET: Supervisors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supervisors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupervisorId,Supervisor_Name,Supervisor_Phone_Number,Supervisor_Status,Supervisor_Pay,Supervisor_Hours")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supervisor);
        }

        // GET: Supervisors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supervisor == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupervisorId,Supervisor_Name,Supervisor_Phone_Number,Supervisor_Status,Supervisor_Pay,Supervisor_Hours")] Supervisor supervisor)
        {
            if (id != supervisor.SupervisorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(supervisor.SupervisorId))
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
            return View(supervisor);
        }

        // GET: Supervisors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supervisor == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor
                .FirstOrDefaultAsync(m => m.SupervisorId == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // POST: Supervisors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supervisor == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Supervisor'  is null.");
            }
            var supervisor = await _context.Supervisor.FindAsync(id);
            if (supervisor != null)
            {
                _context.Supervisor.Remove(supervisor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupervisorExists(int id)
        {
          return (_context.Supervisor?.Any(e => e.SupervisorId == id)).GetValueOrDefault();
        }
    }
}
