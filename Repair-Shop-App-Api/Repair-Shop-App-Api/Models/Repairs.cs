using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair_Shop_App_Api.Models
{
    public class Repairs
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Device))]
        public int DeviceId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ProblemDescription { get; set; } = string.Empty;

        [ForeignKey(nameof(StatusStep))]
        public int CurrentStatusId { get; set; }

        [Required]
        public DateTime ReceivedAt { get; set; } = DateTime.Now;

        public DateTime? CompletedAt { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? EstimatedCost { get; set; }


        public Devices Device { get; set; } = null!;
        public StatusSteps StatusStep { get; set; } = null!;
        public ICollection<RepairStatusHistory> RepairStatusHistory { get; set; } = new List<RepairStatusHistory>();



    }
}
