namespace ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById
{
    public class CustomerSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; private set; }

        public Guid ServiceId { get; private set; }
    }
}
