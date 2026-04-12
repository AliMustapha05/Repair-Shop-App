using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Repositories
{
    public class RepairStatusHistoryRepository
    {
        private readonly AppDbContext _context;

        public RepairStatusHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RepairStatusHistory>> GetByRepairIdAsync(int repairId)
        {
            return await _context.RepairStatusHistories
                .Include(h => h.StatusStep)
                .Where(h => h.RepairId == repairId)
                .OrderBy(h => h.ChangedAt)
                .ToListAsync();
        }

        public async Task<RepairStatusHistory> AddAsync(RepairStatusHistory history)
        {
            _context.RepairStatusHistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<List<RepairStatusHistory>> GetLatestAsync(int count)
        {
            return await _context.RepairStatusHistories
                .Include(h => h.StatusStep)
                .OrderByDescending(h => h.ChangedAt)
                .Take(count)
                .ToListAsync();
        }
    }
}