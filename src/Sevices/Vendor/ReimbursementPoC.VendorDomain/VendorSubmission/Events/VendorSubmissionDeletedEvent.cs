using ReimbursementPoC.Vendor.Domain.Common;

namespace ReimbursementPoC.Vendor.Domain.Vendor.Events
{
    public class VendorSubmissionDeletedEvent : DomainEvent
    {
        public VendorSubmissionDeletedEvent(VendorSubmissionEntity vendorSubmission)
        {
            VendorSubmission = vendorSubmission;
        }

        public VendorSubmissionEntity VendorSubmission { get; }
    }
}