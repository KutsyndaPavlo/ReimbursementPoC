namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById
{
    public class CustomerSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }

        public bool IsCanceled { get; private set; }

        public string VendorName { get; set; }

        public string ServiceFullName { get; set; }

        public string? Description { get; set; }
    }
}
