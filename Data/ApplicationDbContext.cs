using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using logindemo.Models;

namespace logindemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Seed default ticket data with static values
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1, // Static integer ID
                    TicketId = "a3f2b5c1-7eae-4d11-8e56-3f5ec5a2bfa2", // Static GUID as string
                    PoliceStation = "Central Station",
                    ReporterName = "Jane Doe",
                    Issue = "Unauthorized Access",
                    Description = "Reported unauthorized access to the secure area.",
                    Priority = "High",
                    Status = "Open",
                    ImagePath = null
                }
            );
        }
    }
}
