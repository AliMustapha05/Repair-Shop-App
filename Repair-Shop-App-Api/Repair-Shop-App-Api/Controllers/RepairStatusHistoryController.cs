using Microsoft.AspNetCore.Mvc;
using Repair_Shop_App_Api.DTOs.RepairStatusHistory;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Services;

namespace Repair_Shop_App_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepairStatusHistoryController : ControllerBase
    {
        private readonly RepairsService _service;

        private readonly RepairStatusHistoryService _historyService;

        public RepairStatusHistoryController(RepairsService service, RepairStatusHistoryService historyService)
        {
            _service = service;
            _historyService = historyService;
        }

        // =========================
        // GET HISTORY BY REPAIR ID
        // =========================
        [HttpGet("{repairId}")]
        public async Task<ActionResult> GetByRepair(int repairId)
        {
            var history = await _service.GetHistoryAsync(repairId);

            var result = history.Select(h => new RepairStatusHistoryDto
            {
                Id = h.Id,
                RepairId = h.RepairId,
                StatusStepId = h.StatusStepId,
                Note = h.Note,
                ChangedAt = h.ChangedAt
            });

            return Ok(result);
        }

        // =========================
        // ADD STATUS HISTORY
        // =========================
        [HttpPost]
        public async Task<ActionResult> Create(CreateRepairStatusHistoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ FIX: correct naming
            var model = new RepairStatusHistory
            {
                RepairId = dto.RepairId,
                StatusStepId = dto.StatusStepId, 
                Note = dto.Note,
                ChangedAt = DateTime.Now
            };

            var created = await _service.AddStatusAsync(model); 

            return Ok(created);
        }

        [HttpGet("latest")]
        public async Task<ActionResult> GetLatest()
        {
            var data = await _historyService.GetLatestAsync(10);

            var result = data.Select(x => new
            {
                x.RepairId,
                StatusName = x.StatusStep.Name,
                x.ChangedAt
            });

            return Ok(result);
        }
    }
}