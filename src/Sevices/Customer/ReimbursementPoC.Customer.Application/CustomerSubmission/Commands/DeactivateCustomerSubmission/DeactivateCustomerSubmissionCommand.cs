using MediatR;
using ReimbursementPoC.Customer.Application.Customer.Queries.GetCustomerById;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.DeactivateCustomer
{
    public class DeactivateCustomerSubmissionCommand : IRequest<CustomerSubmissionDto>
    {
        public Guid Id { get; set; }
    }
}
