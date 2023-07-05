namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById
{
    public class VendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; private set; }

        public Guid ServiceId { get; private set; }
    }
}
