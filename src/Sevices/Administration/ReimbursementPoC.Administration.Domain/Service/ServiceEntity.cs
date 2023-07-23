using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Service.Events;

namespace ReimbursementPoC.Administration.Domain.Service
{
    public class ServiceEntity : BaseEntity, IAggregateRoot
    {
        private ServiceEntity()
        {
            // only for EF
        }

        private ServiceEntity(string name, string? description, Guid programId) : base()
        {
            Name = name;
            Description = description;
            ProgramId = programId;
            IsCanceled = false;
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public bool IsCanceled { get; private set; }

        public Guid ProgramId { get; private set; }

        public ProgramEntity? Program { get; private set; }

        public static ServiceEntity CreateNew(string name, string? description, ProgramEntity program)
        {
            return new ServiceEntity(name, description, program.Id);
        }

        public void Cancel()
        {
            IsCanceled = true;
            this.LastModified = DateTime.UtcNow;
            this.LastModifiedBy = "";
            this.AddDomainEvent(new ServiceCanceledEvent(this));
        }

        public bool CanBeDeleted()
        {
            // ToDo
            return true;
        }

        public void Update(string name, string? description)
        {
            // Rules
            this.Name = name;
            this.Description = description;
            this.LastModified = DateTime.UtcNow;
            this.LastModifiedBy = "";

            this.AddDomainEvent(new ServiceUpdatedEvent(this));
        }
    }
}
