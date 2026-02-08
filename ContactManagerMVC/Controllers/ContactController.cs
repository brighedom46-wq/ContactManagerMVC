// Import the Models namespace to access Contact, Category, and ContactContext
using ContactManagerMVC.Models;

// Import ASP.NET Core MVC functionality
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerMVC.Controllers
{
    // Controller responsible for handling Contact-related actions
    public class ContactController : Controller
    {
        // Database context used to interact with the database
        private ContactContext context { get; set; }

        // Constructor that receives the ContactContext via dependency injection
        public ContactController(ContactContext ctx)
        {
            context = ctx;
        }

        // ===================== ADD CONTACT (GET) =====================
        // Displays the Add Contact form
        [HttpGet]
        public IActionResult Add()
        {
            // Used by the view to determine the current action
            ViewBag.Action = "Add";

            // Retrieve all categories from the database and sort them alphabetically
            // Used to populate the category dropdown list
            ViewBag.Categories = context.Categories
                                         .OrderBy(c => c.Title)
                                         .ToList();

            // Reuse the Edit view and pass in a new empty Contact object
            return View("Edit", new Contact());
        }

        // ===================== CONTACT DETAILS (GET) =====================
        // Displays the details of a specific contact
        [HttpGet]
        public IActionResult Detail(int id, string slug)
        {
            // Find the contact by its primary key (ContactId)
            var contact = context.Contacts.Find(id);

            // If the contact does not exist, show the NotFound view
            if (contact == null)
            {
                return View("NotFound");
            }

            // Set the action name for the view
            ViewBag.Action = "Detail";

            // Load categories (optional, keeps layout consistent)
            ViewBag.Categories = context.Categories
                                         .OrderBy(c => c.Title)
                                         .ToList();

            // Pass the contact object to the Detail view
            return View(contact);
        }

        // ===================== EDIT CONTACT (GET) =====================
        // Displays the Edit Contact form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Retrieve the contact from the database
            var contact = context.Contacts.Find(id);

            // If the contact does not exist, show the NotFound view
            if (contact == null)
            {
                return View("NotFound");
            }

            // Set the action name for the view
            ViewBag.Action = "Edit";

            // Retrieve categories for the dropdown list
            ViewBag.Categories = context.Categories
                                         .OrderBy(c => c.Title)
                                         .ToList();

            // Pass the contact to the Edit view
            return View(contact);
        }

        // ===================== ADD / EDIT CONTACT (POST) =====================
        // Handles form submission for both adding and editing contacts
        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            // Check if all validation rules pass
            if (ModelState.IsValid)
            {
                // If ContactId is 0, this is a new contact
                if (contact.ContactId == 0)
                {
                    context.Contacts.Add(contact);
                }
                // Otherwise, update the existing contact
                else
                {
                    context.Contacts.Update(contact);
                }

                // Save changes to the database
                context.SaveChanges();

                // Redirect to the Home page after successful save
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Set the correct action name if validation fails
                ViewBag.Action = (contact.ContactId == 0) ? "Add" : "Edit";

                // Reload categories so the dropdown does not break
                ViewBag.Categories = context.Categories
                                             .OrderBy(c => c.Title)
                                             .ToList();

                // Return the form with validation error messages
                return View(contact);
            }
        }

        // ===================== DELETE CONTACT (GET) =====================
        // Displays the delete confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Set the action name for the view
            ViewBag.Action = "Delete";

            // Load categories (optional, for layout consistency)
            ViewBag.Categories = context.Categories
                                         .OrderBy(c => c.Title)
                                         .ToList();

            // Find the contact to be deleted
            var contact = context.Contacts.Find(id);

            // Pass the contact to the Delete view
            return View(contact);
        }

        // ===================== DELETE CONTACT (POST) =====================
        // Performs the actual delete operation
        [HttpPost]
        public IActionResult Delete(Contact contact)
        {
            // Remove the contact from the database
            context.Contacts.Remove(contact);

            // Save changes
            context.SaveChanges();

            // Redirect back to the Home page after deletion
            return RedirectToAction("Index", "Home");
        }
    }
}
