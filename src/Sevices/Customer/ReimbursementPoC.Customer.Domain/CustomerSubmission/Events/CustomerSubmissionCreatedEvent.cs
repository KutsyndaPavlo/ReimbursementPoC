using ReimbursementPoC.Customer.Domain.Common;

namespace ReimbursementPoC.Customer.Domain.Customer.Events
{
    public class CustomerSubmissionCreatedEvent : DomainEvent
    {
        public CustomerSubmissionCreatedEvent(CustomerSubmissionEntity customerSubmission)
        {
            CustomerSubmission = customerSubmission;
        }

        public CustomerSubmissionEntity CustomerSubmission { get; }
    }
}