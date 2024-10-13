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
    public class AppointmentsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public AppointmentsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Appointments
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set up sort parameters for customers
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer";

            // If a search has been performed, reset the page number to 1
            if (searchString != null)
            {
                pageNumber = 1; // Reset to the first page when a new search is made
            }
            else
            {
                // If no new search is made, retain the current filter string
                searchString = currentFilter;
            }

            // Check if the Appointment context is null
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Appointment' is null."); // Return an error if it is
            }

            // Store the current search string for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Query to retrieve appointments, including related customer, vehicle, and employee data
            var appointments = from app in _context.Appointment
                               .Include(m => m.Customer)
                               .Include(m => m.Vehicle)
                               .Include(m => m.Employee)
                               select app;

            // If the search string is not empty, filter the appointments based on various fields
            if (!String.IsNullOrEmpty(searchString))
            {
                appointments = appointments.Where(m =>
                    m.Customer.FirstName.Contains(searchString) || // Search by customer's first name
                    m.Customer.LastName.Contains(searchString) ||  // Search by customer's last name
                    (m.Customer.FirstName + " " + m.Customer.LastName).Contains(searchString) || // Search by full customer name
                    m.Employee.FirstName.Contains(searchString) ||  // Search by employee's first name
                    m.Employee.LastName.Contains(searchString) ||   // Search by employee's last name
                    (m.Employee.FirstName + " " + m.Employee.LastName).Contains(searchString) || // Search by full employee name
                    m.Vehicle.Registration.Contains(searchString) || // Search by vehicle registration number
                    m.AppointmentName.Contains(searchString) // Search by appointment name
                );
            }

            // Sort appointments based on the selected sort order
            switch (sortOrder)
            {
                case "Customer":
                    appointments = appointments.OrderBy(s => s.Customer.FirstName).ThenBy(s => s.Customer.LastName); // Ascending order
                    break;
                case "customer_desc":
                    appointments = appointments.OrderByDescending(s => s.Customer.FirstName).ThenByDescending(s => s.Customer.LastName); // Descending order
                    break;
            }

            int pageSize = 10; // Define the number of items per page
                               // Return the paginated list of appointments to the view
            return View(await PaginatedList<Appointment>.CreateAsync(appointments.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Appointments/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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

        // GET: Appointments/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentName,AppointmentDate,CustomerId,VehicleId,EmployeeId,ServiceCost")] Appointment appointment)
        {
            if (ModelState.IsValid)
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


        // GET: Appointments/Edit
        [Authorize(Roles = "Admin,Employee")]
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

        // POST: Appointments/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentName,AppointmentDate,CustomerId,VehicleId,EmployeeId,ServiceCost")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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

        // GET: Appointments/Delete
        [Authorize(Roles = "Admin")]
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

        // POST: Appointments/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Appointment' is null.");
            }

            var appointment = await _context.Appointment
                .Include(a => a.FaultParts)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment != null)
            {
                if (appointment.FaultParts != null && appointment.FaultParts.Any())
                {
                    _context.FaultPart.RemoveRange(appointment.FaultParts);
                }

                _context.Appointment.Remove(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}



