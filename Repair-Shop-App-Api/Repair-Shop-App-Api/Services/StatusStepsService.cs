using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;

namespace Repair_Shop_App_Api.Services
{
    public class StatusStepsService
    {
        private readonly StatusStepsRepository _repository;

        public StatusStepsService(StatusStepsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StatusSteps>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<StatusSteps?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<StatusSteps> CreateAsync(StatusSteps statusStep)
        {
            return await _repository.CreateAsync(statusStep);
        }

        public async Task<StatusSteps?> UpdateAsync(StatusSteps statusStep)
        {
            return await _repository.UpdateAsync(statusStep);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsStepWithSameNameAsync(string name)
        {
            return await _repository.ExistsStepWithSameNameAsync(name);
        }
    }
}