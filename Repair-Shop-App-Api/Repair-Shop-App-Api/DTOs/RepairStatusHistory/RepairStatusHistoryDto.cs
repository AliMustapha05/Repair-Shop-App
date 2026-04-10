namespace Repair_Shop_App_Api.DTOs.RepairStatusHistory
{
    public class RepairStatusHistoryDto
    {
        public int Id { get; set; }

        public int RepairId { get; set; }   
        public int StatusStepId { get; set; }

        public string StatusName { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; }

        public string? Note { get; set; }
    }
}