using MediatR;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerSubmissionCommand : IRequest<CustomerSubmissionDto>
    {
        public Guid CustomerId { get; set; }

        public Guid VendorSubmissionId { get; set; }

        public string VendorName { get; set; }

        public string ServiceFullName { get; set; }

        public string Description { get; set; }
    }
}
