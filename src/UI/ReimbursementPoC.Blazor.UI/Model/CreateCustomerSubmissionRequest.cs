namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CreateCustomerSubmissionRequest
    {
        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }
    }
}
