using ReimbursementPoC.Program.Domain.Common;

namespace ReimbursementPoC.Program.Domain.Program.Events
{
    public class ProgramCreatedEvent : DomainEvent
    {
        public ProgramCreatedEvent(ProgramEntity program)
        {
            Program = program;
        }

        public ProgramEntity Program { get; }
    }
}