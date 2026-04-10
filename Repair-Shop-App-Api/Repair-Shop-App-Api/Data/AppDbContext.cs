using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // =========================
        // DBSets
        // =========================
        public DbSet<DeviceTypes> DeviceTypes { get; set; }
        public DbSet<StatusSteps> StatusSteps { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Repairs> Repairs { get; set; }
        public DbSet<RepairStatusHistory> RepairStatusHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // DEVICE → DEVICE TYPE
            // =========================
            modelBuilder.Entity<Devices>()
                .HasOne(d => d.DeviceType)
                .WithMany(dt => dt.Devices)
                .HasForeignKey(d => d.DeviceTypeId)
                .OnDelete(DeleteBehavior.NoAction)
                ;

            // =========================
            // REPAIR → DEVICE
            // =========================
            modelBuilder.Entity<Repairs>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Repairs)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.NoAction);

            // =========================
            // REPAIR → CURRENT STATUS
            // =========================
            modelBuilder.Entity<Repairs>()
                .HasOne(r => r.CurrentStatus)
                .WithMany(s => s.Repairs)
                .HasForeignKey(r => r.CurrentStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            // =========================
            // HISTORY → REPAIR
            // =========================
            modelBuilder.Entity<RepairStatusHistory>()
                .HasOne(h => h.Repair)
                .WithMany(r => r.StatusHistory)
                .HasForeignKey(h => h.RepairId)
                .OnDelete(DeleteBehavior.NoAction);

            // =========================
            // HISTORY → STATUS STEP
            // =========================
            modelBuilder.Entity<RepairStatusHistory>()
                .HasOne(h => h.StatusStep)
                .WithMany(s => s.StatusHistory)
                .HasForeignKey(h => h.StatusStepId)
                .OnDelete(DeleteBehavior.NoAction);

            // =========================
            // SEED DATA (REQUIRED BY PDF)
            // =========================

            modelBuilder.Entity<DeviceTypes>().HasData(
                new DeviceTypes { Id = 1, Name = "Mobile", IsActive = true },
                new DeviceTypes { Id = 2, Name = "Laptop", IsActive = true },
                new DeviceTypes { Id = 3, Name = "Tablet", IsActive = true },
                new DeviceTypes { Id = 4, Name = "Desktop", IsActive = true }
            );

            modelBuilder.Entity<StatusSteps>().HasData(
                new StatusSteps { Id = 1, Name = "Received", SortOrder = 1, IsActive = true },
                new StatusSteps { Id = 2, Name = "Diagnosed", SortOrder = 2, IsActive = true },
                new StatusSteps { Id = 3, Name = "Waiting for Parts", SortOrder = 3, IsActive = true },
                new StatusSteps { Id = 4, Name = "In Progress", SortOrder = 4, IsActive = true },
                new StatusSteps { Id = 5, Name = "Quality Check", SortOrder = 5, IsActive = true },
                new StatusSteps { Id = 6, Name = "Ready for Pickup", SortOrder = 6, IsActive = true },
                new StatusSteps { Id = 7, Name = "Returned", SortOrder = 7, IsActive = true },
                new StatusSteps { Id = 8, Name = "Cancelled", SortOrder = 8, IsActive = true }
            );
        }
    }
}