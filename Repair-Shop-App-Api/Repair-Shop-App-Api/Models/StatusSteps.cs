using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Repair_Shop_App_Api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class StatusSteps
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<Repairs> Repairs { get; set; } = new List<Repairs>();
        public ICollection<RepairStatusHistory> StatusHistory { get; set; } = new List<RepairStatusHistory>();
    }
}
