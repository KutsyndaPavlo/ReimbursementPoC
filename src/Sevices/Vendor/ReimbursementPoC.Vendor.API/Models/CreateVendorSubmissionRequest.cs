namespace ReimbursementPoC.Vendor.API.Models
{
    public class CreateVendorSubmissionRequest
    {
        public Guid ServiceId { get; set; }

        public Guid VendorId { get; set; }

        public string ServiceFullName { get; set; }

        public string Description { get; set; }
    }
}
