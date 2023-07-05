using ReimbursementPoC.Program.Domain.Common;

namespace ReimbursementPoC.Program.Domain.Program.Events
{
    public class ProgramDeletedEvent : DomainEvent
    {
        public ProgramDeletedEvent(ProgramEntity program)
        {
            Program = program;
        }

        public ProgramEntity Program { get; }
    }
}