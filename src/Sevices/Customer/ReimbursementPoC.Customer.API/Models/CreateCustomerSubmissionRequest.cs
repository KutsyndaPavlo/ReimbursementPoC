namespace ReimbursementPoC.Customer.API.Models
{
    public class CreateCustomerRequest
    {
        public Guid CustomerId { get; private set; }

        public Guid VendorSubmissionId { get; private set; }
    }
}
