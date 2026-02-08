// Provides attributes used for validation and display metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactManagerMVC.Models
{
    // Represents a Contact entity in the application
    public class Contact
    {
        // Primary key for the Contact table
        public int ContactId { get; set; }

        // First name is required
        [Required(ErrorMessage = "User must have a First Name.")]
        public string FirstName { get; set; }

        // Last name is required
        [Required(ErrorMessage = "User must have a Last Name.")]
        public string LastName { get; set; }

        // Phone number is required
        [Required(ErrorMessage = "User must have a Phone number.")]

        // Ensures phone number follows the format: xxx-xxx-xxxx
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$",
            ErrorMessage = "Phone number must be in the format xxx-xxx-xxxx."
        )]
        public string Phone { get; set; }

        // Email address is required
        [Required(ErrorMessage = "User must have a Email.")]

        // Ensures a valid email format
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        // Optional organization name (nullable)
        public string? Organization { get; set; }

        // Stores the date the contact was added
        public DateTime DateAdded { get; set; }

        // Foreign key linking Contact to Category
        // Ensures a category is selected
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        // Navigation property for the related Category
        public Category? Category { get; set; }

        // Read-only property used for SEO-friendly URLs
        // Generates a slug based on First and Last Name
        public string Slug =>
            $"{FirstName ?? ""} {LastName ?? ""}"
                .Trim()
                .Replace(' ', '-')
                .ToLower();
    }
}
