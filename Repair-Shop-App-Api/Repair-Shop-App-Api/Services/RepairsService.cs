using Repair_Shop_App_Api.DTOs.Repairs;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;

namespace Repair_Shop_App_Api.Services
{
    public class RepairsService
    {
        private readonly RepairsRepository _repository;

        private readonly RepairStatusHistoryRepository _historyRepository;

        public RepairsService(
            RepairsRepository repository,
            RepairStatusHistoryRepository historyRepository
        )
        {
            _repository = repository;
            _historyRepository = historyRepository;
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

        public async Task<List<RepairStatusHistory>> GetHistoryAsync(int repairId)
        {
            return await _historyRepository.GetByRepairIdAsync(repairId);
        }

        public async Task<RepairStatusHistory> AddStatusAsync(RepairStatusHistory history)
        {
            return await _repository.AddStatusAsync(history);
        }

        public async Task<bool> ExistsActiveRepairForDeviceAsync(int deviceId)
        {
            return await _repository.ExistsActiveRepairForDeviceAsync(deviceId);
        }

        public async Task<Repairs?> UpdateAsync(int id, UpdateRepairDto dto)
        {
            var repair = await _repository.GetByIdAsync(id);

            if (repair == null)
                return null;

            repair.DeviceId = dto.DeviceId;
            repair.ProblemDescription = dto.ProblemDescription;
            repair.CurrentStatusId = dto.CurrentStatusId;

            await _repository.UpdateAsync(repair);

            return repair;
        }

        
    }
}