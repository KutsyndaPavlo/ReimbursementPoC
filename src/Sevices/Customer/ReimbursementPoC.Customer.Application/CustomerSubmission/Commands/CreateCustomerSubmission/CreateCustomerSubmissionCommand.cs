using MediatR;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerSubmissionCommand : IRequest<CustomerSubmissionDto>
    {
        public Guid CustomerId { get; private set; }

        public Guid VendorSubmissionId { get; private set; }
    }
}
