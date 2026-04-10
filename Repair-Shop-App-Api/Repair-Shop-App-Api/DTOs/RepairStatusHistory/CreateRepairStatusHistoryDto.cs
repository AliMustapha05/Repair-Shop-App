namespace Repair_Shop_App_Api.DTOs.RepairStatusHistory
{
    public class CreateRepairStatusHistoryDto
    {
        public int RepairId { get; set; }
        public int StatusStepId { get; set; }
        public string? Note { get; set; }
    }
}