using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Repositories
{
    public class RepairsRepository
    {
        private readonly AppDbContext _context;

        public RepairsRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET all repairs
        public async Task<List<Repairs>> GetAllAsync()
        {
            return await _context.Repairs
                .Include(r => r.Device)           // Include the device details
                .Include(r => r.StatusStep)    // Include current status
                .ToListAsync();
        }

        // GET repair by ID (with history)
        public async Task<Repairs?> GetByIdAsync(int id)
        {
            return await _context.Repairs
                .Include(r => r.Device)
                .Include(r => r.StatusStep)
                .Include(r => r.RepairStatusHistory) // Include all status history
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // POST: create a new repair
        public async Task<Repairs> CreateAsync(Repairs repair)
        {
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

        // POST status update to repair
        public async Task<RepairStatusHistory> AddStatusAsync(RepairStatusHistory statusHistory)
        {
            _context.RepairStatusHistory.Add(statusHistory);
            await _context.SaveChangesAsync();
            return statusHistory;
        }

        public async Task<bool> ExistsActiveRepairForDeviceAsync(int deviceId)
        {
            // Returns true if there is an active (not completed) repair for the device
            return await _context.Repairs.AnyAsync(r => r.DeviceId == deviceId && r.CompletedAt == null);
        }
    }
}