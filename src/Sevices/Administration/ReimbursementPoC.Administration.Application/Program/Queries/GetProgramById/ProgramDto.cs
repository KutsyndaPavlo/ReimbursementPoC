namespace ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById
{
    public class ProgramDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string StateId { get;  set; }

        public string State { get; set; }

        public DateTime StartDate { get;  set; }

        public DateTime EndDate { get;  set; }

        public DateTime Created { get;  set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsCanceled { get; set; }
    }
}
