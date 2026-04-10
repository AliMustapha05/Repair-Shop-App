using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair_Shop_App_Api.Models
{
    public class RepairStatusHistory
    {
        [Key]
        public int Id { get; set; }

        // FK → Repair
        [ForeignKey(nameof(Repair))]
        public int RepairId { get; set; }

        // FK → StatusStep
        [ForeignKey(nameof(StatusStep))]
        public int StatusStepId { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Note { get; set; }

        // 🔹 Navigation Properties
        public Repairs Repair { get; set; } = null!;

        public StatusSteps StatusStep { get; set; } = null!;
    }
}