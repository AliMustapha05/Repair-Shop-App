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

        public RepairStatusHistoryController(RepairsService service)
        {
            _service = service;
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
    }
}