using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Data
{
    // EF Core database context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tables / DbSets
        public DbSet<DeviceTypes> DeviceTypes { get; set; }
        public DbSet<StatusSteps> StatusSteps { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Repairs> Repairs { get; set; }
        public DbSet<RepairStatusHistory> RepairStatusHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent cascade delete مشاكل (multiple cascade paths)

            modelBuilder.Entity<Repairs>()
                .HasOne(r => r.StatusStep)
                .WithMany()
                .HasForeignKey(r => r.CurrentStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RepairStatusHistory>()
                .HasOne(r => r.Repair)
                .WithMany()
                .HasForeignKey(r => r.RepairId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RepairStatusHistory>()
                .HasOne(r => r.StatusStep)
                .WithMany()
                .HasForeignKey(r => r.StatusStepsId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }

   
}