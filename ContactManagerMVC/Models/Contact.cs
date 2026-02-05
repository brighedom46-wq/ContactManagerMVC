using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactManagerMVC.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "User must have a First Name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User must have a Last Name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User must have a Phone number.")]

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$",
            ErrorMessage = "Phone number must be in the format xxx-xxx-xxxx."
        )]
        public string Phone { get; set; }

        [Required(ErrorMessage = "User must have a Email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string? Organization { get; set; }
        public DateTime DateAdded { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string Slug =>
            $"{FirstName ?? ""} {LastName ?? ""}"?.Trim().Replace(' ', '-').ToLower();
    }
}
