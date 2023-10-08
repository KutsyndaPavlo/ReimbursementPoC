namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CustomerSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }

        public string VendorName { get; set; }

         public string ServiceFullName { get; set; }

        public string? Description { get; set; }

        public bool IsCanceled { get; set; }
    }
}
