using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Customer.Application.Common.Interfaces;
using ReimbursementPoC.Customer.Domain;
using ReimbursementPoC.Customer.Domain.Product;
using ReimbursementPoC.Customer.Domain.Customer.Events;
using ReimbursementPoC.Customer.Domain.CustomerSubmission.Specification;

namespace ReimbursementPoC.Customer.Application.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerSubmissionCommandHandler : IRequestHandler<DeleteCustomerSubmissionCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteCustomerSubmissionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteCustomerSubmissionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.CustomerSubmissions.FirstOrDefaultAsync(new CustomerSubmissionByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new CustomerSubmissionNotFoundException($"Customer with id {command.Id} doesn't exist");
            }

            if (!entity.CanBeDeleted())
            {
                throw new CustomerSubmissionCanNotBeDeletedException($"Customer with id {command.Id} can't be deleted");
            }

            _applicationDbContext.CustomerSubmissions.Remove(entity);

            entity.AddDomainEvent(new CustomerSubmissionDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
