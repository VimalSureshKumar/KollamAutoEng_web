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
    // Restrict access to users with "Admin" or "Employee" roles
    [Authorize(Roles = "Admin,Employee")]
    public class PartsController : Controller
    {
        private readonly KollamAutoEng_webContext _context; // Database context for accessing data

        // Constructor to initialize the context
        public PartsController(KollamAutoEng_webContext context)
        {
            _context = context; // Assign the database context
        }

        // GET: Parts
        [Authorize(Roles = "Admin,Employee")] // Ensure that only users with Admin or Employee roles can access this action
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            // Check if the Part context is null
            if (_context.Part == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Part' is null."); // Return an error if it is null
            }

            // Store the current search string for use in the view
            ViewData["CurrentFilter"] = searchString;

            // Query to retrieve all parts from the database
            var parts = from par in _context.Part
                        select par;

            // If the search string is not empty, filter the parts based on the reference or part name
            if (!String.IsNullOrEmpty(searchString))
            {
                parts = parts.Where(m =>
                    m.Reference.Contains(searchString) || // Search by part reference
                    m.PartName.Contains(searchString) // Search by part name
                );
            }

            int pageSize = 10; // Define the number of items per page
            // Return the paginated list of parts to the view
            return View(await PaginatedList<Part>.CreateAsync(parts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Parts/Details
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or the Part context is null
            if (id == null || _context.Part == null)
            {
                return NotFound(); // Return NotFound if no id is provided
            }

            // Fetch the part based on its id
            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound(); // Return NotFound if the part doesn't exist
            }

            return View(part); // Return the view with the part details
        }

        // GET: Parts/Create
        [Authorize(Roles = "Admin,Employee")]
        public IActionResult Create()
        {
            // Return the view for creating a new part
            return View();
        }

        // POST: Parts/Create
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken] // Validate anti-forgery token to prevent CSRF attacks
        public async Task<IActionResult> Create([Bind("PartId,Reference,PartName,Cost")] Part part)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Check for an existing part with the same Reference or PartName
                var existingPart = await _context.Part
                    .FirstOrDefaultAsync(p => p.Reference == part.Reference || p.PartName == part.PartName);

                if (existingPart != null)
                {
                    // Add an error message to the model state if a duplicate is found
                    ModelState.AddModelError("Reference", "A part with this reference or name already exists.");
                }
                else
                {
                    _context.Add(part); // Add the new part to the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                    return RedirectToAction(nameof(Index)); // Redirect to the index action
                }
            }
            // Return the view with validation errors if the model state is not valid
            return View(part);
        }

        // GET: Parts/Edit
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null or the Part context is null
            if (id == null || _context.Part == null)
            {
                return NotFound(); // Return NotFound if no id is provided
            }

            // Fetch the part based on its id for editing
            var part = await _context.Part.FindAsync(id);
            if (part == null)
            {
                return NotFound(); // Return NotFound if the part doesn't exist
            }
            return View(part); // Return the view for editing the part
        }

        // POST: Parts/Edit
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken] // Validate anti-forgery token to prevent CSRF attacks
        public async Task<IActionResult> Edit(int id, [Bind("PartId,Reference,PartName,Cost")] Part part)
        {
            // Check if the id matches the model's id
            if (id != part.PartId)
            {
                return NotFound(); // Return NotFound if the ids do not match
            }

            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(part); // Update the part in the context
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issues if the part no longer exists
                    if (!PartExists(part.PartId))
                    {
                        return NotFound(); // Return NotFound if the part doesn't exist
                    }
                    else
                    {
                        throw; // Re-throw the exception for handling elsewhere
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index action
            }
            // Return the view with validation errors if the model state is not valid
            return View(part);
        }

        // GET: Parts/Delete
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or the Part context is null
            if (id == null || _context.Part == null)
            {
                return NotFound(); // Return NotFound if no id is provided
            }

            // Fetch the part based on its id for deletion confirmation
            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.PartId == id);
            if (part == null)
            {
                return NotFound(); // Return NotFound if the part doesn't exist
            }

            return View(part); // Return the view for confirming deletion
        }

        // POST: Parts/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken] // Validate anti-forgery token to prevent CSRF attacks
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the Part context is null
            if (_context.Part == null)
            {
                return Problem("Entity set 'KollamAutoEng_webContext.Part' is null."); // Return an error if it is null
            }

            // Fetch the part based on its id
            var part = await _context.Part.FindAsync(id);
            if (part != null)
            {
                _context.Part.Remove(part); // Remove the part from the context
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index action
        }

        // Helper method to check if a part exists by its id
        private bool PartExists(int id)
        {
            return (_context.Part?.Any(e => e.PartId == id)).GetValueOrDefault(); // Check for existence
        }
    }
}
