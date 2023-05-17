using ReimbursementPoC.Program.Domain.Common;

namespace ReimbursementPoC.Program.Domain.Program.Events
{
    public class ProgramDeletedEvent : DomainEvent
    {
        public ProgramDeletedEvent(ProgramEntity product)
        {
            Product = product;
        }

        public ProgramEntity Product { get; }
    }
}