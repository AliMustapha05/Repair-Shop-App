using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs.Repairs;
using Repair_Shop_App_Api.DTOs.RepairStatusHistory;
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

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();

            var result = list.Select(r => new RepairDto
            {
                Id = r.Id,
                DeviceId = r.DeviceId,
                DeviceModel = r.Device?.Model, // optional but useful

                CurrentStatusId = r.CurrentStatusId,
                CurrentStatusName = r.CurrentStatus?.Name,

                ProblemDescription = r.ProblemDescription,

                EstimatedCost = r.EstimatedCost,

                CreatedAt = r.ReceivedAt,   

                CompletedAt = r.CompletedAt,
                Notes = r.Notes
            });

            return Ok(result);
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var r = await _service.GetByIdAsync(id);
            if (r == null) return NotFound();

            return Ok(new RepairDto
            {
                Id = r.Id,
                DeviceId = r.DeviceId,
                DeviceModel = r.Device?.Model,

                CurrentStatusId = r.CurrentStatusId,
                CurrentStatusName = r.CurrentStatus?.Name,

                ProblemDescription = r.ProblemDescription,

                EstimatedCost = r.EstimatedCost,

                CreatedAt = r.ReceivedAt,   

                CompletedAt = r.CompletedAt,
                Notes = r.Notes
            });
        }

        // =========================
        // CREATE REPAIR
        // =========================
        [HttpPost]
        public async Task<ActionResult> Create(CreateRepairDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await _service.ExistsActiveRepairForDeviceAsync(dto.DeviceId);
            if (exists)
                return Conflict(new { message = "Active repair already exists" });

            var model = new Repairs
            {
                DeviceId = dto.DeviceId,
                ProblemDescription = dto.ProblemDescription,
                CurrentStatusId = dto.CurrentStatusId ?? 0,
                ReceivedAt = DateTime.Now
            };

            var created = await _service.CreateAsync(model);

            return Ok(new RepairDto
            {
                Id = created.Id,
                DeviceId = created.DeviceId,
                CurrentStatusId = created.CurrentStatusId,
                ProblemDescription = created.ProblemDescription,

                CreatedAt = created.ReceivedAt   
            });
        }

        // =========================
        // ADD STATUS TO REPAIR
        // =========================
        [HttpPost("{id}/status")]
        public async Task<ActionResult> AddStatus(int id, CreateRepairStatusHistoryDto dto)
        {
            var model = new RepairStatusHistory
            {
                RepairId = id,
                StatusStepId = dto.StatusStepId,
                Note = dto.Note,
                ChangedAt = DateTime.Now
            };

            var created = await _service.AddStatusAsync(model);

            return Ok(new
            {
                created.Id,
                created.RepairId,
                created.StatusStepId,
                created.Note,
                created.ChangedAt
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRepairDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}