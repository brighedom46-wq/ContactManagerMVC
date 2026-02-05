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

            modelBuilder.Entity<Contact>()
                .Property(c => c.DateAdded)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 101, Title = "Family" },
                new Category { CategoryId = 102, Title = "Friend" },
                new Category { CategoryId = 103, Title = "Work" }
                );
        }
    }
}
