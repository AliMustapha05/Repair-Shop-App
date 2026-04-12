using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs.StatusSteps;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Services;

namespace Repair_Shop_App_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusStepsController : ControllerBase
    {
        private readonly StatusStepsService _service;

        public StatusStepsController(StatusStepsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateStatusStepDto dto)
        {
            if (await _service.ExistsStepWithSameNameAsync(dto.Name))
                return Conflict("Step already exists");

            var created = await _service.CreateAsync(new StatusSteps
            {
                Name = dto.Name,
                SortOrder = dto.SortOrder,
                IsActive = true
            });

            return Ok(created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _service.GetByIdAsync(id);

            if (exists == null)
                return NotFound("Status step not found");

            // optional safety check (recommended)
            var isUsed = await _service.IsUsedInRepairsAsync(id);

            if (isUsed)
                return Conflict("Cannot delete: status step is used in repairs");

            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return BadRequest("Delete failed");

            return NoContent();
        }
    }
}