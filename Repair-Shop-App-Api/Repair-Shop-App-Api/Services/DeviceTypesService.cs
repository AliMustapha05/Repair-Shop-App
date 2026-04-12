using Repair_Shop_App_Api.DTOs.DeviceTypes;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;

namespace Repair_Shop_App_Api.Services
{
    public class DeviceTypesService
    {
        private readonly DeviceTypesRepository _repository;

        public DeviceTypesService(DeviceTypesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DeviceTypeDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(x => new DeviceTypeDto
            {
                Id = x.Id,
                Name = x.Name,
                Icon = x.Icon ?? "📦"
            }).ToList();
        }

        public async Task<DeviceTypes?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<DeviceTypes> CreateAsync(DeviceTypes deviceType)
        {
            return await _repository.CreateAsync(deviceType);
        }

        public async Task<DeviceTypes?> UpdateAsync(DeviceTypes deviceType)
        {
            return await _repository.UpdateAsync(deviceType);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _repository.ExistsByNameAsync(name);
        }
    }
}