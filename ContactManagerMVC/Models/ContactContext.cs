// Provides Entity Framework Core functionality
using Microsoft.EntityFrameworkCore;

namespace ContactManagerMVC.Models
{
    // Database context class used by Entity Framework Core
    // Handles database connections and maps models to tables
    public class ContactContext : DbContext
    {
        // Constructor that receives database configuration options
        // These options are passed to the base DbContext class
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        { }

        // DbSet representing the Contacts table in the database
        public DbSet<Contact> Contacts { get; set; }

        // DbSet representing the Categories table in the database
        public DbSet<Category> Categories { get; set; }

        // Configures the model and seeds initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base DbContext implementation
            base.OnModelCreating(modelBuilder);

            // ===================== CATEGORY SEED DATA =====================
            // Adds default categories to the database
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Title = "Family" },
                new Category { CategoryId = 2, Title = "Friend" },
                new Category { CategoryId = 3, Title = "Work" }
            );

            // ===================== DEFAULT DATE CONFIGURATION =====================
            // Automatically sets DateAdded to the current date
            // when a new Contact record is created
            modelBuilder.Entity<Contact>()
                .Property(c => c.DateAdded)
                .HasDefaultValueSql("GETDATE()");

            // ===================== CONTACT SEED DATA =====================
            // Adds sample contact records to the database
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Shuai",
                    LastName = "Gao",
                    Phone = "587-876-0888",
                    Email = "gaoshuai099@gmail.com",
                    CategoryId = 1
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Bright",
                    LastName = "Edom",
                    Phone = "111-222-3333",
                    Email = "Bright.Edom@gmail.com",
                    CategoryId = 2
                }
            );
        }
    }
}
