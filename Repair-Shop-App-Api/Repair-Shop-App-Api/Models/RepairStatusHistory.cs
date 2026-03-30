using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair_Shop_App_Api.Models
{
    public class RepairStatusHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Repair))]
        public int RepairId { get; set; }

        [ForeignKey(nameof(StatusStep))]
        public int StatusStepsId { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string Note { get; set; } = string.Empty;

        public Repairs Repair { get; set; } = null!;
        public StatusSteps StatusStep { get; set; } = null!;


    }
}
