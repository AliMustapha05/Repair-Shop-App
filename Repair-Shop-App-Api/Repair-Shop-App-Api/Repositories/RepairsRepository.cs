using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.DTOs.Repairs;
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

        public async Task<List<Repairs>> GetAllAsync()
        {
            return await _context.Repairs
                .Include(r => r.Device)
                .Include(r => r.CurrentStatus)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Repairs?> GetByIdAsync(int id)
        {
            return await _context.Repairs
                .Include(r => r.Device)
                .Include(r => r.CurrentStatus)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Repairs> CreateAsync(Repairs repair)
        {
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

        public async Task<RepairStatusHistory> AddStatusAsync(RepairStatusHistory history)
        {
            // 1. Add history
            _context.RepairStatusHistories.Add(history);

            // 2. 🔥 UPDATE MAIN REPAIR STATUS
            var repair = await _context.Repairs.FindAsync(history.RepairId);

            if (repair != null)
            {
                repair.CurrentStatusId = history.StatusStepId;
            }

            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<bool> ExistsActiveRepairForDeviceAsync(int deviceId)
        {
            return await _context.Repairs
                .AnyAsync(r => r.DeviceId == deviceId && r.CompletedAt == null);
        }

        public async Task<Repairs?> UpdateAsync(Repairs repair)
        {
            var existing = await _context.Repairs.FindAsync(repair.Id);

            if (existing == null)
                return null;

            existing.DeviceId = repair.DeviceId;
            existing.ProblemDescription = repair.ProblemDescription;
            existing.CurrentStatusId = repair.CurrentStatusId;

            await _context.SaveChangesAsync();

            return existing;
        }

        
    }
}