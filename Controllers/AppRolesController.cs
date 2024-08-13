using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KollamAutoEng_web.Models; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KollamAutoEng_web.Controllers
{
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList(); // Convert to list to avoid deferred execution issues
            return View(roles); // Pass the roles to the view
        }

        public IActionResult Create()
        {
            return View(new RoleViewModel()); // Return an empty RoleViewModel
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Name) && !(await _roleManager.RoleExistsAsync(model.Name)))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Name));
                return RedirectToAction("Index");
            }

            // If the model is invalid or role already exists, redisplay the form with an error message
            ModelState.AddModelError("", "Role creation failed or role already exists.");
            return View(model);
        }
    }
}
