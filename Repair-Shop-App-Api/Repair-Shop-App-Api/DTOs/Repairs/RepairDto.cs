namespace Repair_Shop_App_Api.DTOs
{
    public class RepairDto
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }          
        public int CurrentStatusId { get; set; }   

        public string ProblemDescription { get; set; } = string.Empty;

        public DateTime? CompletedAt { get; set; }
        public string? Notes { get; set; }
    }
}