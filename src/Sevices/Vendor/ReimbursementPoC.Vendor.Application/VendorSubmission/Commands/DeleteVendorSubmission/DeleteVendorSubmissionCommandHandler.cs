using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Domain;
using ReimbursementPoC.Vendor.Domain.Product;
using ReimbursementPoC.Vendor.Domain.Vendor.Events;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.Specification;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.DeleteVendor
{
    public class DeleteVendorSubmissionCommandHandler : IRequestHandler<DeleteVendorSubmissionCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteVendorSubmissionCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteVendorSubmissionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.VendorSubmissions.FirstOrDefaultAsync(new VendorSubmissionByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new VendorSubmissionNotFoundException($"Vendor with id {command.Id} doesn't exist");
            }

            if (!entity.CanBeDeleted())
            {
                throw new VendorSubmissionCanNotBeDeletedException($"Vendor with id {command.Id} can't be deleted");
            }

            _applicationDbContext.VendorSubmissions.Remove(entity);

            entity.AddDomainEvent(new VendorSubmissionDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
