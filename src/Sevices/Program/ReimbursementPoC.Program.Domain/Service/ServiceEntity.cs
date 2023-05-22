using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Domain.Service
{
    public class ServiceEntity : BaseEntity
    {
        private ServiceEntity(string name, string? description, Guid programId)
        {
            Name = name;
            Description = description;
            ProgramId = programId;
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public Guid ProgramId { get; private set; }

        public ProgramEntity Program { get; private set; }

        public static ServiceEntity CreateNew(string name, string? description, ProgramEntity program)
        {
            return new ServiceEntity(name, description, program.Id);
        }
    }
}
