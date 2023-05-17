using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Domain.Product.Events
{
    public class ProgramCreatedEvent : DomainEvent
    {
        public ProgramCreatedEvent(ProgramEntity product)
        {
            Product = product;
        }

        public ProgramEntity Product { get; }
    }
}