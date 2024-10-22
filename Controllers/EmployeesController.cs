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
    [Authorize(Roles = "Admin,Employee")] // Restricts access to the controller for Admin and Employee roles
    public class EmployeesController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context for accessing employee data

        public EmployeesController(KollamAutoEng_webContext context)
        {
            _context = context; // Initialize the context
        }

        // GET: Employees
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder; // Store the current sort order for use in the view
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "last_name_desc" : "LastName"; // Set up sort parameters for last name sorting

            // Reset the page number if a new search is performed
            if (searchString != null)
            {
                pageNumber = 1; // Reset to the first page when a new search is made
            }
            else
            {
                // Retain the current filter string for pagination
                searchString = currentFilter;
            }

            // Check if the Employee context is null
            if (_context.Employee == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Employee' is null."); // Return an error if it is
            }

            ViewData["CurrentFilter"] = searchString; // Store the current search string for use in the view

            // Query to retrieve employees
            var employees = from emp in _context.Employee
                            select emp;

            // If the search string is not empty, filter the employees based on various fields
            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(m =>
                    m.FirstName.Contains(searchString) || // Search by employee's first name
                    m.LastName.Contains(searchString) ||  // Search by employee's last name
                    (m.FirstName + " " + m.LastName).Contains(searchString) || // Search by full employee name
                    m.PhoneNumber.Contains(searchString) // Search by phone number
                );
            }

            // Sort employees based on the selected sort order
            switch (sortOrder)
            {
                case "LastName":
                    employees = employees.OrderBy(c => c.LastName); // Ascending order
                    break;
                case "last_name_desc":
                    employees = employees.OrderByDescending(c => c.LastName); // Descending order
                    break;
            }

            int pageSize = 10; // Define the number of items per page
            // Return the paginated list of employees to the view
            return View(await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Employees/Details
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or the Employee context is null
            if (id == null || _context.Employee == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            // Fetch the employee based on the provided id
            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound(); // Return NotFound if no employee is found
            }

            return View(employee); // Return the details view with the employee data
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public IActionResult Create()
        {
            return View(); // Return the create view
        }

        // POST: Employees/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,PhoneNumber,Status,Pay,Hours")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Check for existing employees with the same FirstName, LastName, and PhoneNumber
                var existingEmployee = await _context.Employee
                    .FirstOrDefaultAsync(e => e.FirstName == employee.FirstName
                                               && e.LastName == employee.LastName
                                               && e.PhoneNumber == employee.PhoneNumber);

                if (existingEmployee != null) // If a duplicate is found
                {
                    // Add an error message to the model state
                    ModelState.AddModelError("PhoneNumber", "An employee with the same name and phone number already exists.");
                }
                else
                {
                    _context.Add(employee); // Add the new employee to the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the index action
                }
            }
            return View(employee); // Return the view with validation errors
        }

        // GET: Employees/Edit
        [Authorize(Roles = "Admin")] // Restricts access to users with the Admin role
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null or the Employee context is null
            if (id == null || _context.Employee == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            // Find the employee by id
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound(); // Return NotFound if no employee is found
            }
            return View(employee); // Return the edit view with the employee data
        }

        // POST: Employees/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")] // Restricts access to users with the Admin role
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,PhoneNumber,Status,Pay,Hours")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound(); // Return NotFound if the ids do not match
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee); // Update the employee in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound(); // Return NotFound if the employee no longer exists
                    }
                    else
                    {
                        throw; // Throw exception if another error occurred
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index action after successful edit
            }
            return View(employee); // Return the view with validation errors
        }

        // GET: Employees/Delete
        [Authorize(Roles = "Admin")] // Restricts access to users with the Admin role
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or the Employee context is null
            if (id == null || _context.Employee == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            // Find the employee by id
            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound(); // Return NotFound if no employee is found
            }

            return View(employee); // Return the delete confirmation view with the employee data
        }

        // POST: Employees/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] // Restricts access to users with the Admin role
        [ValidateAntiForgeryToken] // Prevents CSRF attacks
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the Employee context is null
            if (_context.Employee == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Employee' is null."); // Return an error if it is
            }

            // Find the employee along with related appointments and fault parts
            var employee = await _context.Employee
                .Include(e => e.Appointments) // Include related appointments
                .ThenInclude(a => a.FaultParts) // Include related fault parts for each appointment
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee != null)
            {
                // Remove related fault parts
                foreach (var appointment in employee.Appointments)
                {
                    if (appointment.FaultParts?.Any() == true)
                    {
                        _context.FaultPart.RemoveRange(appointment.FaultParts);
                    }
                }

                // Remove related appointments
                if (employee.Appointments?.Any() == true)
                {
                    _context.Appointment.RemoveRange(employee.Appointments);
                }

                // Remove the employee
                _context.Employee.Remove(employee);

                await _context.SaveChangesAsync(); // Save changes to the database
            }

            return RedirectToAction(nameof(Index)); // Redirect to the index action after successful deletion
        }

        // Helper method to check if an employee exists by id
        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault(); // Returns true if an employee with the given id exists
        }
    }
}
