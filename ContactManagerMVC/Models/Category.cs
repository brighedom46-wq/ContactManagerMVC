namespace ContactManagerMVC.Models
{
    // Represents a category that groups contacts
    // Examples: Family, Friend, Work
    public class Category
    {
        // Primary key for the Category table
        public int CategoryId { get; set; }

        // Name/title of the category
        public string Title { get; set; }
    }
}
