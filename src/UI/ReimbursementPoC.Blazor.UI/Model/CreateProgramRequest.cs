using System.ComponentModel.DataAnnotations;

namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CreateProgramRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int? StateId { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime? EndDate { get; set; }
    }
}
