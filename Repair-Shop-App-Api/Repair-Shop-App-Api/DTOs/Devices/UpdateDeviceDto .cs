namespace Repair_Shop_App_Api.DTOs.Devices
{
    public class UpdateDeviceDto
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string? SerialNumber { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerPhone { get; set; } = string.Empty;
        public int DeviceTypeId { get; set; }
    }
}