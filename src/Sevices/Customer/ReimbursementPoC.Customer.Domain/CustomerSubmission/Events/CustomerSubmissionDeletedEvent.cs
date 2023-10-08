using ReimbursementPoC.Customer.Domain.Common;

namespace ReimbursementPoC.Customer.Domain.Customer.Events
{
    public class CustomerSubmissionDeletedEvent : DomainEvent
    {
        public CustomerSubmissionDeletedEvent(CustomerSubmissionEntity customerSubmission)
        {
            CustomerSubmission = customerSubmission;
        }

        public CustomerSubmissionEntity CustomerSubmission { get; }
    }
}