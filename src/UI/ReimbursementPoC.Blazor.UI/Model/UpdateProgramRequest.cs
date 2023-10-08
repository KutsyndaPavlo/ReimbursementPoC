using System.ComponentModel.DataAnnotations;

namespace ReimbursementPoC.Blazor.UI.Model
{
    public class UpdateProgramRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastModified { get; set; }
    }
}
