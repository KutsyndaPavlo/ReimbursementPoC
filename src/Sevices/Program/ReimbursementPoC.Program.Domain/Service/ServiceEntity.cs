using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Domain.Service
{
    public class ServiceEntity : BaseEntity, IAggregateRoot
    {
        private ServiceEntity()
        {

        }

        private ServiceEntity(string name, string? description, Guid programId) : base()
        {
            Name = name;
            Description = description;
            ProgramId = programId;
            IsActive = true;
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public Guid ProgramId { get; private set; }

        public ProgramEntity? Program { get; private set; }

        public static ServiceEntity CreateNew(string name, string? description, ProgramEntity program)
        {
            return new ServiceEntity(name, description, program.Id);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Update(string name, string? description)
        {
            // Rules
            this.Name = name;
            this.Description = description;
            this.LastModified = DateTime.UtcNow;
            this.LastModifiedBy = "";
        }
    }
}
