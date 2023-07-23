using System.ComponentModel.DataAnnotations;

namespace ReimbursementPoC.Blazor.UI.Model
{ 
    public class ProgramDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int StateId { get; set; }

        public string State { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsCanceled { get; set; }
    }
}
