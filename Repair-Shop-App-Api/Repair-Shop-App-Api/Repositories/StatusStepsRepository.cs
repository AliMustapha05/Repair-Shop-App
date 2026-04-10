using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Repositories
{
    public class StatusStepsRepository
    {
        private readonly AppDbContext _context;

        public StatusStepsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatusSteps>> GetAllAsync()
        {
            return await _context.StatusSteps.ToListAsync();
        }

        public async Task<StatusSteps?> GetByIdAsync(int id)
        {
            return await _context.StatusSteps.FindAsync(id);
        }

        public async Task<StatusSteps> CreateAsync(StatusSteps statusStep)
        {
            _context.StatusSteps.Add(statusStep);
            await _context.SaveChangesAsync();
            return statusStep;
        }

        public async Task<StatusSteps?> UpdateAsync(StatusSteps statusStep)
        {
            var existing = await _context.StatusSteps.FindAsync(statusStep.Id);
            if (existing == null) return null;

            existing.Name = statusStep.Name;
            existing.Description = statusStep.Description;
            existing.SortOrder = statusStep.SortOrder;
            existing.IsActive = statusStep.IsActive;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.StatusSteps.FindAsync(id);
            if (existing == null) return false;

            _context.StatusSteps.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsStepWithSameNameAsync(string name)
        {
            return await _context.StatusSteps.AnyAsync(x => x.Name == name);
        }
    }
}