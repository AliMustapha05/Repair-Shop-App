namespace Repair_Shop_App_Api.DTOs.Repairs
{
    public class RepairDto
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }
        public string? DeviceModel { get; set; }   // ⭐ useful for UI

        public int CurrentStatusId { get; set; }
        public string? CurrentStatusName { get; set; } // ⭐ VERY IMPORTANT

        public string ProblemDescription { get; set; } = string.Empty;

        public decimal? EstimatedCost { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public string? Notes { get; set; }
    }
}