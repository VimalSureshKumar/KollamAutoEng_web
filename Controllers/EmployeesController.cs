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
    public class EmployeesController : Controller
    {
        private readonly KollamAutoEng_webContext _context;

        public EmployeesController(KollamAutoEng_webContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Authorize(Roles = "Admin,Employee")] // Restricts access to users with the Admin or Employee role
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            // Store the current sort order for use in the view
            ViewData["CurrentSort"] = sortOrder;

            // Set up sort parameters for last name sorting
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "last_name_desc" : "LastName";

            // If a new search is performed, reset the page number to 1
            if (searchString != null)
            {
                pageNumber = 1; // Reset to the first page when a new search is made
            }
            else
            {
                // If no new search is made, retain the current filter string
                searchString = currentFilter;
            }

            // Check if the Employee context is null
            if (_context.Employee == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Employee' is null."); // Return an error if it is
            }

            // Store the current search string for use in the view
            ViewData["CurrentFilter"] = searchString;

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
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,PhoneNumber,Status,Pay,Hours")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,PhoneNumber,Status,Pay,Hours")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Employee' is null.");
            }

            var employee = await _context.Employee
                .Include(e => e.Appointments)
                .ThenInclude(a => a.FaultParts)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);

            if (employee != null)
            {
                foreach (var appointment in employee.Appointments)
                {
                    if (appointment.FaultParts?.Any() == true)
                    {
                        _context.FaultPart.RemoveRange(appointment.FaultParts);
                    }
                }

                if (employee.Appointments?.Any() == true)
                {
                    _context.Appointment.RemoveRange(employee.Appointments);
                }

                _context.Employee.Remove(employee);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
