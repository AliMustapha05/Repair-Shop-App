namespace Repair_Shop_App_Api.DTOs.Repairs
{
    public class UpdateRepairDto
    {
        public int DeviceId { get; set; }
        public string? ProblemDescription { get; set; }
        public int CurrentStatusId { get; set; }
    }
}