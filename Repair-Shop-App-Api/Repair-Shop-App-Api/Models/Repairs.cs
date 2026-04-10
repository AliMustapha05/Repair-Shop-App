using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair_Shop_App_Api.Models
{
    public class Repairs
    {
        [Key]
        public int Id { get; set; }

        // FK → Device
        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ProblemDescription { get; set; } = string.Empty;

        // FK → Current Status
        [ForeignKey(nameof(CurrentStatus))]
        public int CurrentStatusId { get; set; }

        [Required]
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? EstimatedCost { get; set; }

        // 🔹 Navigation Properties
        public Devices Device { get; set; } = null!;

        public StatusSteps CurrentStatus { get; set; } = null!;

        public ICollection<RepairStatusHistory> StatusHistory { get; set; } = new List<RepairStatusHistory>();
    }
}