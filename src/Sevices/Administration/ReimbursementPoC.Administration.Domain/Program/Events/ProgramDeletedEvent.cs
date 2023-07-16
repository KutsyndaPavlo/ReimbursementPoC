using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Events
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