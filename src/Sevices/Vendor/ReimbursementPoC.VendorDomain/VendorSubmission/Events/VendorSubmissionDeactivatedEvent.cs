using ReimbursementPoC.Vendor.Domain.Common;

namespace ReimbursementPoC.Vendor.Domain.Vendor.Events
{
    public class VendorSubmissionDeactivatedEvent : DomainEvent
    {
        public VendorSubmissionDeactivatedEvent(VendorSubmissionEntity vendorSubmission)
        {
            VendorSubmission = vendorSubmission;
        }

        public VendorSubmissionEntity VendorSubmission { get; }
    }
}
