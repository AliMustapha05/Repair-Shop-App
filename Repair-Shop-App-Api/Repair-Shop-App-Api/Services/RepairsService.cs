using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;

namespace Repair_Shop_App_Api.Services
{
    public class RepairsService
    {
        private readonly RepairsRepository _repository;

        public RepairsService(RepairsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Repairs>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Repairs?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Repairs> CreateAsync(Repairs repair)
        {
            return await _repository.CreateAsync(repair);
        }

        public async Task<RepairStatusHistory> AddStatusAsync(RepairStatusHistory history)
        {
            return await _repository.AddStatusAsync(history);
        }

        public async Task<bool> ExistsActiveRepairForDeviceAsync(int deviceId)
        {
            return await _repository.ExistsActiveRepairForDeviceAsync(deviceId);
        }
    }
}