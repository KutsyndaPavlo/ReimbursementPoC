using ReimbursementPoC.Vendor.Domain.Common;

namespace ReimbursementPoC.Vendor.Domain.Vendor.Events
{
    public class VendorSubmissionCreatedEvent : DomainEvent
    {
        public VendorSubmissionCreatedEvent(VendorSubmissionEntity vendorSubmission)
        {
            VendorSubmission = vendorSubmission;
        }

        public VendorSubmissionEntity VendorSubmission { get; }
    }
}