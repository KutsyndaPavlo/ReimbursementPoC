using System;
namespace ReimbursementPoC.Vendor.IntergrationEvents
{
    public static class Constants
    {
        public const string VendorSubmissionCreatedTopic = "vendorsubmission-created";
        public const string VendorSubmissionCanceledTopic = "vendorsubmission-canceled";
        public const string VendorSubmissionDeletedTopic = "vendorsubmission-deleted";
        public const string VendorSubmissionCreatedSubscription = "vendorsubmission-created-sub";
        public const string VendorSubmissionCanceledSubscription = "vendorsubmission-canceled-sub";
        public const string VendorSubmissionDeletedSubscription = "vendorsubmission-deleted-sub";
    }
}
