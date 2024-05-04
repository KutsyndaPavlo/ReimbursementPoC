using MediatR;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerSubmissionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
