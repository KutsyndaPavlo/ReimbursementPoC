namespace ReimbursementPoC.Program.Application.Services.Queries.GetServiceById
{
    public class ServiceDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; private set; }

        public Guid ProgramId { get; private set; }

        public DateTime Created { get; private set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModified { get; protected set; }

        public string? LastModifiedBy { get; set; }
    }
}
