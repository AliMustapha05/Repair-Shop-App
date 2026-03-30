using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair_Shop_App_Api.Models
{
    public class Devices
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(DeviceType))]
        public int DeviceTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Model { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? SerialNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength (200)]
        public string OwnerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string OwnerPhone { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public DeviceTypes DeviceType { get; set; } = null!;

    }
}
