using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs;
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
            var list = await _service.GetAllAsync();

            var result = list.Select(s => new StatusStepDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description ?? "",
                SortOrder = s.SortOrder,
                IsActive = s.IsActive
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StatusStepDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _service.ExistsStepWithSameNameAsync(dto.Name))
            {
                return Conflict(new { message = "A status step with this name already exists." });
            }

            var model = new StatusSteps
            {
                Name = dto.Name,
                Description = dto.Description ?? "",
                SortOrder = dto.SortOrder,
                IsActive = dto.IsActive
            };

            var created = await _service.CreateAsync(model);
            dto.Id = created.Id;

            return Created("", dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StatusStepDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var model = new StatusSteps
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description ?? "",
                SortOrder = dto.SortOrder,
                IsActive = dto.IsActive
            };

            var updated = await _service.UpdateAsync(model);
            if (updated == null) return NotFound();

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok();
        }
    }
}