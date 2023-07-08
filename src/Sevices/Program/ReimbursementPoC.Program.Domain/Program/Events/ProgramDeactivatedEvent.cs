using ReimbursementPoC.Program.Domain.Common;

namespace ReimbursementPoC.Program.Domain.Program.Events
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
