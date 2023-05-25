using ReimbursementPoC.Program.Domain.Common;

namespace ReimbursementPoC.Program.Domain.Program.Events
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