using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Customer>()
                .HasIndex(x => x.UserName)
                .IsUnique();
            modelBuilder.Entity<Entities.Customer>()
                .HasIndex(x => x.EmailAddress)
                .IsUnique();
        }
    }
}
