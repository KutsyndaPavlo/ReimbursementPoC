using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Events
{
    public class ProgramDeactivatedEvent : DomainEvent
    {
        public ProgramDeactivatedEvent(ProgramEntity program)
        {
            Program = program;
        }

        public ProgramEntity Program { get; }
    }
}
