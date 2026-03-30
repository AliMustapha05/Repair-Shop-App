namespace Repair_Shop_App_Api.DTOs.RepairsStatusHistory
{
    public class RepairStatusHistoryDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }
        public int StatusStepsId { get; set; }   
        public string? Note { get; set; } 
    }
}
