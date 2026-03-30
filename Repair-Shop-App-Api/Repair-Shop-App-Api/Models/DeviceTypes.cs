using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Repair_Shop_App_Api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class DeviceTypes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; } = true;


        public ICollection<Devices> Devices { get; set; } = new List<Devices>();

    }
}
