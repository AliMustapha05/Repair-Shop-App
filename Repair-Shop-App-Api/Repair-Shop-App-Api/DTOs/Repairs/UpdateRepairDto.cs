namespace Repair_Shop_App_Api.DTOs.Repairs
{
    public class UpdateRepairDto
    {
        public int CurrentStatusId { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string? Notes { get; set; }
    }
}
