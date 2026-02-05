using Microsoft.EntityFrameworkCore;

namespace ContactManagerMVC.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Title = "Family" },
                new Category { CategoryId = 2, Title = "Friend" },
                new Category { CategoryId = 3, Title = "Work" }
                );

            modelBuilder.Entity<Contact>()
                .Property(c => c.DateAdded)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Contact>().HasData(
                new Contact {
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
