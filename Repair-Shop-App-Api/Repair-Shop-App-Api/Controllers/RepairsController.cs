using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs;
using Repair_Shop_App_Api.DTOs.Repairs;
using Repair_Shop_App_Api.DTOs.RepairsStatusHistory;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Services;

namespace Repair_Shop_App_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepairsController : ControllerBase
    {
        private readonly RepairsService _service;

        public RepairsController(RepairsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Create(RepairDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await _service.ExistsActiveRepairForDeviceAsync(dto.DeviceId);
            if (exists)
            {
                return Conflict(new { message = "There is already an active repair for this device." });
            }

            var model = new Repairs
            {
                DeviceId = dto.DeviceId,
                ProblemDescription = dto.ProblemDescription,
                CurrentStatusId = dto.CurrentStatusId,
                ReceivedAt = DateTime.Now
            };

            var created = await _service.CreateAsync(model);
            dto.Id = created.Id;

            return Created("", dto);
        }

        [HttpPost("{id}/status")]
        public async Task<ActionResult> AddStatus(int id, RepairStatusHistoryDto dto)
        {
            var model = new RepairStatusHistory
            {
                RepairId = id,
                StatusStepsId = dto.StatusStepsId,
                Note = dto.Note ?? "",
                ChangedAt = DateTime.Now
            };

            var created = await _service.AddStatusAsync(model);

            return Ok(created);
        }
    }
}