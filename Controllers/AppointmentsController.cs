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
    [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
    public class AppointmentsController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        // Constructor that accepts the database context
        public AppointmentsController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Appointments
        [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Set up sort parameters for appointments
            ViewData["CustomerSortParm"] = sortOrder == "Customer" ? "customer_desc" : "Customer";

            // Reset the page number if a new search is performed
            if (searchString != null)
            {
                pageNumber = 1; // Reset to the first page when a new search is made
            }
            else
            {
                // Retain the current filter string if no new search is made
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
                               .Include(m => m.Customer) // Include related Customer data
                               .Include(m => m.Vehicle) // Include related Vehicle data
                               .Include(m => m.Employee) // Include related Employee data
                               select app;

            // Filter appointments based on search string
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
        [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return NotFound if ID is null
            }

            // Retrieve the appointment including related data
            var appointment = await _context.Appointment
                .Include(a => a.Customer) // Include related Customer data
                .Include(a => a.Employee) // Include related Employee data
                .Include(a => a.Vehicle) // Include related Vehicle data
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound(); // Return NotFound if the appointment is not found
            }

            return View(appointment); // Return the details view with the appointment data
        }

        // GET: Appointments/Create
        [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
        public IActionResult Create()
        {
            // Populate dropdowns for related entities
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration");
            return View(); // Return the create view
        }

        // POST: Appointments/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
        [ValidateAntiForgeryToken] // Validate the anti-forgery token
        public async Task<IActionResult> Create([Bind("AppointmentId,AppointmentName,AppointmentDate,CustomerId,VehicleId,EmployeeId,ServiceCost")] Appointment appointment)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                // Check for existing appointments for the same customer, date, and appointment name
                var existingAppointment = await _context.Appointment
                    .FirstOrDefaultAsync(a => a.CustomerId == appointment.CustomerId &&
                                               a.AppointmentDate == appointment.AppointmentDate &&
                                               a.AppointmentName == appointment.AppointmentName);

                if (existingAppointment != null) // If a duplicate appointment is found
                {
                    // Add an error message to the model state
                    ModelState.AddModelError("AppointmentDate", "An appointment with the same name for this customer already exists on the selected date.");
                }
                else
                {
                    _context.Add(appointment); // Add the new appointment to the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the Index action
                }
            }

            // If we reach this point, something failed; re-populate the view data for the dropdowns
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", appointment.EmployeeId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", appointment.VehicleId);
            return View(appointment); // Return the view with validation errors
        }

        // GET: Appointments/Edit
        [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound(); // Return NotFound if ID is null or context is null
            }

            var appointment = await _context.Appointment.FindAsync(id); // Find the appointment by ID
            if (appointment == null)
            {
                return NotFound(); // Return NotFound if the appointment is not found
            }

            // Populate dropdowns for related entities
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", appointment.EmployeeId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", appointment.VehicleId);
            return View(appointment); // Return the edit view with the appointment data
        }

        // POST: Appointments/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restricts access to Admin and Employee roles
        [ValidateAntiForgeryToken] // Validate the anti-forgery token
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,AppointmentName,AppointmentDate,CustomerId,VehicleId,EmployeeId,ServiceCost")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound(); // Return NotFound if the ID does not match the appointment ID
            }

            if (ModelState.IsValid) // Check if the model state is valid
            {
                try
                {
                    _context.Update(appointment); // Update the appointment in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException) // Handle concurrency exceptions
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound(); // Return NotFound if the appointment does not exist
                    }
                    else
                    {
                        throw; // Re-throw the exception if it is a different error
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }

            // If we reach this point, something failed; re-populate the view data for the dropdowns
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "FirstName", appointment.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", appointment.EmployeeId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Registration", appointment.VehicleId);
            return View(appointment); // Return the view with validation errors
        }

        // GET: Appointments/Delete
        [Authorize(Roles = "Admin")] // Restricts access to Admin role
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound(); // Return NotFound if ID is null or context is null
            }

            // Retrieve the appointment including related data
            var appointment = await _context.Appointment
                .Include(a => a.Customer) // Include related Customer data
                .Include(a => a.Employee) // Include related Employee data
                .Include(a => a.Vehicle) // Include related Vehicle data
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound(); // Return NotFound if the appointment is not found
            }

            return View(appointment); // Return the delete view with the appointment data
        }

        // POST: Appointments/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] // Restricts access to Admin role
        [ValidateAntiForgeryToken] // Validate the anti-forgery token
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Appointment' is null."); // Return an error if the context is null
            }

            // Retrieve the appointment including related FaultParts data
            var appointment = await _context.Appointment
                .Include(a => a.FaultParts) // Include related FaultParts data
                .FirstOrDefaultAsync(m => m.AppointmentId == id);

            if (appointment != null)
            {
                // Remove related FaultParts if any exist
                if (appointment.FaultParts != null && appointment.FaultParts.Any())
                {
                    _context.FaultPart.RemoveRange(appointment.FaultParts);
                }

                _context.Appointment.Remove(appointment); // Remove the appointment from the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }

            return NotFound(); // Return NotFound if the appointment is not found
        }

        // Check if an appointment exists by ID
        private bool AppointmentExists(int id)
        {
            return (_context.Appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault(); // Check for existence
        }
    }
}
