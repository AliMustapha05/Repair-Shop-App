namespace Repair_Shop_App_Api.DTOs.Repairs
{
    public class CreateRepairDto
    {
        public int DeviceId { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
        public int? CurrentStatusId { get; set; }
    }
}
