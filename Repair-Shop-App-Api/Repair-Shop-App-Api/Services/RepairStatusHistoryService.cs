using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;

namespace Repair_Shop_App_Api.Services
{
    public class RepairStatusHistoryService
    {
        private readonly RepairStatusHistoryRepository _repository;

        public RepairStatusHistoryService(RepairStatusHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RepairStatusHistory>> GetByRepairIdAsync(int repairId)
        {
            return await _repository.GetByRepairIdAsync(repairId);
        }

        public async Task<RepairStatusHistory> AddAsync(RepairStatusHistory history)
        {
            return await _repository.AddAsync(history);
        }

        public async Task<List<RepairStatusHistory>> GetLatestAsync(int count)
        {
            return await _repository.GetLatestAsync(count);
        }
    }
}