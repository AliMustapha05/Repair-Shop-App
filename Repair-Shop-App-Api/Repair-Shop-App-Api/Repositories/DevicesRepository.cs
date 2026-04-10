using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;

namespace Repair_Shop_App_Api.Repositories
{
    public class DevicesRepository
    {
        private readonly AppDbContext _context;

        public DevicesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Devices>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.DeviceType)
                .ToListAsync();
        }

        public async Task<Devices?> GetByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.DeviceType)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Devices> CreateAsync(Devices device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<Devices?> UpdateAsync(Devices device)
        {
            var existing = await _context.Devices.FindAsync(device.Id);
            if (existing == null) return null;

            existing.DeviceTypeId = device.DeviceTypeId;
            existing.Brand = device.Brand;
            existing.Model = device.Model;
            existing.SerialNumber = device.SerialNumber;
            existing.OwnerName = device.OwnerName;
            existing.OwnerPhone = device.OwnerPhone;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> ExistsBySerialNumberAsync(string serialNumber)
        {
            return await _context.Devices.AnyAsync(d => d.SerialNumber == serialNumber);
        }
    }
}