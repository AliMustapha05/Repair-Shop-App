using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Repositories
{
    public class DeviceTypesRepository
    {
        private readonly AppDbContext _context;

        public DeviceTypesRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all device types
        public async Task<List<DeviceTypes>> GetAllAsync()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

        // Get a device type by Id
        public async Task<DeviceTypes?> GetByIdAsync(int id)
        {
            return await _context.DeviceTypes.FindAsync(id);
        }

        // Create a new device type
        public async Task<DeviceTypes> CreateAsync(DeviceTypes deviceType)
        {
            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();
            return deviceType;
        }

        // Update an existing device type
        public async Task<DeviceTypes?> UpdateAsync(DeviceTypes deviceType)
        {
            var existing = await _context.DeviceTypes.FindAsync(deviceType.Id);
            if (existing == null) 
                return null;

            existing.Name = deviceType.Name;
            existing.IsActive = deviceType.IsActive;

            await _context.SaveChangesAsync();
            return existing;
        }

        // Delete a device type
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.DeviceTypes.FindAsync(id);
            if (existing == null) 
                return false;

            _context.DeviceTypes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.DeviceTypes.AnyAsync(dt => dt.Name == name);
        }
    }
}