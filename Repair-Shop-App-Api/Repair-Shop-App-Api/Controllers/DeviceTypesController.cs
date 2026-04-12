using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateDeviceTypeDto dto)
        {
            if (await _service.ExistsByNameAsync(dto.Name))
                return Conflict("Name already exists");

            var created = await _service.CreateAsync(new DeviceTypes
            {
                Name = dto.Name,
                Icon = string.IsNullOrWhiteSpace(dto.Icon) ? "📦" : dto.Icon,
                IsActive = true
            });

            return Ok(created);
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