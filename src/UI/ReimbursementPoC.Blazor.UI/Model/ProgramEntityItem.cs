using System.ComponentModel.DataAnnotations;

namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CreateProgramRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

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

    public class ProgramEntityItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string State { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastModified { get; set; }
    }

    public class ProgramEntity
    {
        public IEnumerable<ProgramEntityItem> Items { get; set; }
    }


    
}
