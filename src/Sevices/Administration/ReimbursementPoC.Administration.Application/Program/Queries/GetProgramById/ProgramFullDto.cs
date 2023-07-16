using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById
{
    public class ProgramFullDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string State { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime Created { get; private set; }

        public string? CreatedBy { get; set; }

        public DateTime LastModified { get; protected set; }

        public string? LastModifiedBy { get; set; }
        
        //public IEnumerable<ServiceDto> Services { get;}
    }
}
