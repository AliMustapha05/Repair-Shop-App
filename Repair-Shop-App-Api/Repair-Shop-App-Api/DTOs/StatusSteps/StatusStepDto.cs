namespace Repair_Shop_App_Api.DTOs.StatusSteps
{
    public class StatusStepDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; }
    }
}