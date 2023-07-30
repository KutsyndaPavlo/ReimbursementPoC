using ReimbursementPoC.Customer.Domain.Common;

namespace ReimbursementPoC.Customer.Domain.Customer.Events
{
    public class CustomerSubmissionDeactivatedEvent : DomainEvent
    {
        public CustomerSubmissionDeactivatedEvent(CustomerSubmissionEntity customerSubmission)
        {
            CustomerSubmission = customerSubmission;
        }

        public CustomerSubmissionEntity CustomerSubmission { get; }
    }
}
