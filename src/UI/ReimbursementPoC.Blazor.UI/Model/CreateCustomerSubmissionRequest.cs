namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CreateCustomerSubmissionRequest
    {
        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }

        public string VendorName { get; set; }

        public string ServiceFullName { get; set; }

        public string? Description { get; set; }
    }
}
