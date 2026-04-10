namespace Repair_Shop_App_Api.DTOs.DeviceTypes
{
    public class CreateDeviceTypeDto
    {
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}