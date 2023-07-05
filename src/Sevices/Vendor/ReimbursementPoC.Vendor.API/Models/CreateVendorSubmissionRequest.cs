namespace ReimbursementPoC.Vendor.API.Models
{
    public class CreateVendorRequest
    {
        public Guid VendorId { get; private set; }

        public Guid ServiceId { get; private set; }
    }
}
