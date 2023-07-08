namespace ReimbursementPoC.Vendor.API.Models
{
    public class CreateVendorSubmissionRequest
    {
        public Guid VendorId { get; private set; }

        public Guid ServiceId { get; private set; }
    }
}
