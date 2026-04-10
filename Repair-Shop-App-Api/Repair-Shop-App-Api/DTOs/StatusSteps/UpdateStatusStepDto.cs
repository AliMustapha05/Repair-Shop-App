namespace Repair_Shop_App_Api.DTOs.StatusSteps
{
    public class UpdateStatusStepDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; }
    }
}