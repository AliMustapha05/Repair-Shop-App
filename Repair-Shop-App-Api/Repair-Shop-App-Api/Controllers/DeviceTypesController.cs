using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs.DeviceTypes;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Services;

namespace Repair_Shop_App_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceTypesController : ControllerBase
    {
        private readonly DeviceTypesService _service;

        public DeviceTypesController(DeviceTypesService service)
        {
            _service = service;
        }

        // GET: api/DeviceTypes
        [HttpGet]
        public async Task<ActionResult<List<DeviceTypeDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            var dtoList = list.Select(d => new DeviceTypeDto
            {
                Id = d.Id,
                Name = d.Name,
                IsActive = d.IsActive
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/DeviceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceTypeDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(new DeviceTypeDto
            {
                Id = item.Id,
                Name = item.Name,
                IsActive = item.IsActive
            });
        }

        // POST: api/DeviceTypes
        [HttpPost]
        public async Task<ActionResult<DeviceTypeDto>> Create([FromBody] CreateDeviceTypeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Check if name already exists
            var exists = await _service.ExistsByNameAsync(dto.Name);
            if (exists)
            {
                return Conflict(new { message = "A device type with this name already exists." });
            }

            var model = new DeviceTypes
            {
                Name = dto.Name,
                IsActive = true
            };

            var created = await _service.CreateAsync(model);

            var resultDto = new DeviceTypeDto
            {
                Id = created.Id,
                Name = created.Name,
                IsActive = created.IsActive
            };

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, resultDto);
        }

        // PUT: api/DeviceTypes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceTypeDto>> Update(int id, [FromBody] DeviceTypeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != dto.Id) return BadRequest();

            var updated = await _service.UpdateAsync(new DeviceTypes
            {
                Id = id,
                Name = dto.Name,
                IsActive = dto.IsActive
            });

            if (updated == null) return NotFound();

            return Ok(new DeviceTypeDto
            {
                Id = updated.Id,
                Name = updated.Name,
                IsActive = updated.IsActive
            });
        }

        // DELETE: api/DeviceTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok();
        }
    }
}