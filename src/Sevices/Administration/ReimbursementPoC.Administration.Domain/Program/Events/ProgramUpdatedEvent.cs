using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Events
{
    public class ProgramUpdatedEvent : DomainEvent
    {
        public ProgramUpdatedEvent(ProgramEntity program)
        {
            Program = program;
        }

        public ProgramEntity Program { get; }
    }
}