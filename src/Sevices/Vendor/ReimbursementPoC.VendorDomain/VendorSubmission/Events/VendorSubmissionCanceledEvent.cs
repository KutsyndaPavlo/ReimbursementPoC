using ReimbursementPoC.Vendor.Domain.Common;

namespace ReimbursementPoC.Vendor.Domain.Vendor.Events
{
    public class VendorSubmissionCanceledEvent : DomainEvent
    {
        public VendorSubmissionCanceledEvent(VendorSubmissionEntity vendorSubmission)
        {
            VendorSubmission = vendorSubmission;
        }

        public VendorSubmissionEntity VendorSubmission { get; }
    }
}
