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
using System.Drawing.Printing;

namespace KollamAutoEng_web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public AppointmentsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Appointments
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

            if (_context.Appointment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Appointment' is null.");
            }

            ViewData["CurrentFilter"] = searchString;

            var appointments = from app in _context.Appointment
                               .Include(m => m.Customer)
                               .Include(m => m.Vehicle)
                               .Include(m => m.Employee)
                               select app;

            if (!String.IsNullOrEmpty(searchString))
            {
                appointments = appointments.Where(m =>
                   m.Customer.FirstName.Contains(searchString) ||
                   m.Customer.LastName.Contains(searchString) ||
                   m.Employee.FirstName.Contains(searchString) ||
                   m.Employee.LastName.Contains(searchString) ||
                   m.Vehicle.Registration.Contains(searchString) ||
                   m.ServiceCost.ToString().Contains(searchString)
                       );
            }

            switch (sortOrder)
            {
                case "Customer":
                    appointments = appointments.OrderBy(s => s.Customer.FirstName);
                    appointments = appointments.OrderBy(s => s.Customer.LastName);
                    break;
                case "customer_desc":
                    appointments = appointments.OrderByDescending(s => s.Customer.FirstName);
                    appointments = appointments.OrderByDescending(s => s.Customer.LastName);
                    break;
            }

            var appointmentsList = await appointments.ToListAsync();

            int pageSize = 5;
            return View(await PaginatedList<Appointment>.CreateAsync(appointments.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointment, "AppointmentId", "AppointmentDate");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentDate,CustomerId,VehicleId,EmployeeId,ServiceCost")] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", appointment.EmployeeId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", appointment.VehicleId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", appointment.EmployeeId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", appointment.VehicleId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentDate,CustomerId,VehicleId,EmployeeId,ServiceCost")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", appointment.EmployeeId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", appointment.VehicleId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Customer)
                .Include(a => a.Employee)
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Appointment'  is null.");
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
