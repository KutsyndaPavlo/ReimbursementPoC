using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Domain.Program.Events
{
    public class ProgramDeactivatedEvent : DomainEvent
    {
        public ProgramDeactivatedEvent(ProgramEntity product)
        {
            Product = product;
        }

        public ProgramEntity Product { get; }
    }
}
