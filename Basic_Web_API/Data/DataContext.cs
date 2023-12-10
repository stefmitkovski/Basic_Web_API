using Basic_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Basic_Web_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                    .HasOne(c => c.Company)
                    .WithMany(c => c.Contacts)
                    .HasForeignKey(c => c.CompanyId);
            modelBuilder.Entity<Contact>()
                    .HasOne(c => c.Country)
                    .WithMany(c => c.Contacts)
                    .HasForeignKey(c => c.CountryId);
        }
    }
}
