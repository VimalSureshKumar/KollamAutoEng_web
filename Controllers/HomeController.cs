using KollamAutoEng_web.Areas.Identity.Data;
using KollamAutoEng_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KollamAutoEng_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Logger for the HomeController to log information and errors

        // Constructor that initializes the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; // Assign the logger instance to the private field
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            // Returns the default view for the home page
            return View();
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            // Returns the privacy policy view
            return View();
        }

        // GET: Home/Contact
        public IActionResult Contact()
        {
            // Returns the contact information view
            return View();
        }

        // GET: Home/Formsubmit
        public IActionResult Formsubmit()
        {
            // Returns the form submission view
            return View();
        }

        // GET: Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Disable caching for error responses
        public IActionResult Error()
        {
            // Create an ErrorViewModel instance with the current request ID and return the error view
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
