// Provides access to diagnostic tools such as Activity for error tracking
using System.Diagnostics;

// Import application models (ContactContext, ErrorViewModel)
using ContactManagerMVC.Models;

// Import ASP.NET Core MVC functionality
using Microsoft.AspNetCore.Mvc;

// Import Entity Framework Core features such as Include()
using Microsoft.EntityFrameworkCore;

namespace ContactManagerMVC.Controllers
{
    // Controller responsible for handling Home-related pages
    public class HomeController : Controller
    {
        // Database context used to access the database
        private ContactContext context { get; set; }

        // Constructor that receives ContactContext via dependency injection
        public HomeController(ContactContext ctx)
        {
            context = ctx;
        }

        // ===================== HOME PAGE (INDEX) =====================
        // Displays a list of all contacts on the home page
        public IActionResult Index()
        {
            // Retrieve all contacts from the database
            // Include Category data to avoid lazy loading issues
            // Order contacts alphabetically by First Name
            var contacts = context.Contacts
                                  .Include(c => c.Category)
                                  .OrderBy(c => c.FirstName)
                                  .ToList();

            // Pass the contact list to the Index view
            return View(contacts);
        }

        // ===================== PRIVACY PAGE =====================
        // Displays the Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // ===================== ERROR PAGE =====================
        // Prevents caching of error responses
        // Displays error details when an exception occurs
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create an ErrorViewModel with the current request ID
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
