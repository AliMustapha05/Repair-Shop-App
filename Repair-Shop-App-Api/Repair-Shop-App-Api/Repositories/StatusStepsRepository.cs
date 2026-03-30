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

        // GET all status steps
        public async Task<List<StatusSteps>> GetAllAsync()
        {
            return await _context.StatusSteps.ToListAsync();
        }

        // GET status step by ID
        public async Task<StatusSteps?> GetByIdAsync(int id)
        {
            return await _context.StatusSteps.FindAsync(id);
        }

        // POST: create a new status step
        public async Task<StatusSteps> CreateAsync(StatusSteps statusStep)
        {
            _context.StatusSteps.Add(statusStep);
            await _context.SaveChangesAsync();
            return statusStep;
        }

        // PUT: update a status step by ID
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

        // DELETE: remove a status step by ID
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
            // Returns true if a status step with the same name already exists
            return await _context.StatusSteps.AnyAsync(s => s.Name == name);
        }
    }
}