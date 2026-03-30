using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;

namespace Repair_Shop_App_Api.Services
{
    public class DevicesService
    {
        private readonly DevicesRepository _repository;

        public DevicesService(DevicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Devices>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Devices?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Devices> CreateAsync(Devices device)
        {
            return await _repository.CreateAsync(device);
        }

        public async Task<Devices?> UpdateAsync(Devices device)
        {
            return await _repository.UpdateAsync(device);
        }

        public async Task<bool> ExistsBySerialNumberAsync(string serialNumber)
        {
            return await _repository.ExistsBySerialNumberAsync(serialNumber);
        }
    }
}