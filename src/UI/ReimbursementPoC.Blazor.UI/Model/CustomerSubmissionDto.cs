namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CustomerSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }
    }
}
