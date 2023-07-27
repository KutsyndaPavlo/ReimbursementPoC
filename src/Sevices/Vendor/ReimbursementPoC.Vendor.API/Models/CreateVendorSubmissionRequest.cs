namespace ReimbursementPoC.Vendor.API.Models
{
    public class CreateVendorSubmissionRequest
    {
        public Guid VendorId { get; set; }

        public string VendorName { get; set; }

        public Guid ServiceId { get; set; }

        public string ServiceFullName { get; set; }

        public string? Description { get; set; }
    }
}
