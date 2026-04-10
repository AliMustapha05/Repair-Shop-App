namespace Repair_Shop_App_Api.DTOs.DeviceTypes
{
    public class DeviceTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}