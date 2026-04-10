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
                .AsNoTracking()
                .Select(r => new Repairs
                {
                    Id = r.Id,
                    DeviceId = r.DeviceId,
                    CurrentStatusId = r.CurrentStatusId,
                    ProblemDescription = r.ProblemDescription,
                    CompletedAt = r.CompletedAt,
                    Notes = r.Notes
                })
                .ToListAsync();
        }

        public async Task<Repairs?> GetByIdAsync(int id)
        {
            return await _context.Repairs
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Repairs> CreateAsync(Repairs repair)
        {
            _context.Repairs.Add(repair);
            await _context.SaveChangesAsync();
            return repair;
        }

        public async Task<RepairStatusHistory> AddStatusAsync(RepairStatusHistory statusHistory)
        {
            _context.RepairStatusHistories.Add(statusHistory);
            await _context.SaveChangesAsync();
            return statusHistory;
        }

        public async Task<bool> ExistsActiveRepairForDeviceAsync(int deviceId)
        {
            return await _context.Repairs
                .AnyAsync(r => r.DeviceId == deviceId && r.CompletedAt == null);
        }
    }
}