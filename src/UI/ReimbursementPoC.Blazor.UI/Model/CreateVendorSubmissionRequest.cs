namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CreateVendorSubmissionRequest
    {
        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }

        public string ServiceFullName { get; set; }

        public string Description { get; set; }
    }
}
