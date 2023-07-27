namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById
{
    public class VendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; set; }

        public string VendorName { get; set; }

        public Guid ServiceId { get; set; }

        public string ServiceFullName { get; set; }

        public string Description { get; set; }

        public bool IsCanceled { get; set; }
    }
}
