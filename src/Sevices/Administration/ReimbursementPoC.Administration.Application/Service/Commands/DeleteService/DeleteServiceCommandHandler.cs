using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service.Errors;
using ReimbursementPoC.Administration.Domain.Service.Events;
using ReimbursementPoC.Administration.Domain.Service.Specifications;

namespace ReimbursementPoC.Administration.Application.Services.Commands.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteServiceCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<bool>> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(new ServiceByIdSpecification(command.Id).ToExpression());

            if (service == null)
            {
                return Result<bool>.Failure(ServiceErrors.NotFound(command.Id));
            }

            if (!service.CanBeDeleted())
            {
                return Result<bool>.Failure(ServiceErrors.CanNotBeDeleted(command.Id));
            }

            _applicationDbContext.Services.Remove(service);

            service.AddDomainEvent(new ServiceDeletedEvent(service));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
