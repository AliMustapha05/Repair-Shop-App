using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs.Devices;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Services;

namespace Repair_Shop_App_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DevicesService _service;

        public DevicesController(DevicesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();

            var result = list.Select(d => new DeviceDto
            {
                Id = d.Id,
                DeviceTypeId = d.DeviceTypeId,
                Brand = d.Brand,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                OwnerName = d.OwnerName,
                OwnerPhone = d.OwnerPhone
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var d = await _service.GetByIdAsync(id);
            if (d == null) return NotFound();

            return Ok(new DeviceDto
            {
                Id = d.Id,
                DeviceTypeId = d.DeviceTypeId,
                Brand = d.Brand,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                OwnerName = d.OwnerName,
                OwnerPhone = d.OwnerPhone
            });
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateDeviceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(dto.SerialNumber))
            {
                var exists = await _service.ExistsBySerialNumberAsync(dto.SerialNumber);
                if (exists)
                    return Conflict("Serial number already exists");
            }

            var model = new Devices
            {
                DeviceTypeId = dto.DeviceTypeId,
                Brand = dto.Brand,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                OwnerName = dto.OwnerName,
                OwnerPhone = dto.OwnerPhone,
                CreatedAt = DateTime.Now
            };

            var created = await _service.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateDeviceDto dto)
        {
            var model = new Devices
            {
                Id = id,
                DeviceTypeId = dto.DeviceTypeId,
                Brand = dto.Brand,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                OwnerName = dto.OwnerName,
                OwnerPhone = dto.OwnerPhone,
                CreatedAt = DateTime.Now
            };

            var updated = await _service.UpdateAsync(model);
            if (updated == null) return NotFound();

            return Ok(updated);
        }
    }
}