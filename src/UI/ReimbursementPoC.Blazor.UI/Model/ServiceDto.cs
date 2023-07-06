namespace ReimbursementPoC.Blazor.UI.Model
{
    public class ServiceDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public Guid ProgramId { get; set; }

        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
