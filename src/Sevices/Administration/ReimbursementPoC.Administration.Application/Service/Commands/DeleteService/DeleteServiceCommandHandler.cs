using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Service.Events;
using ReimbursementPoC.Administration.Domain.Service.Exeption;
using ReimbursementPoC.Administration.Domain.Service.Specifications;

namespace ReimbursementPoC.Administration.Application.Services.Commands.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteServiceCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(new ServiceByIdSpecification(command.Id).ToExpression());

            if (service == null)
            {
                throw new ServiceNotFoundException($"Program with id {command.Id} doesn't exist");
            }

            if (!service.CanBeDeleted())
            {
                throw new ServiceCanNotBeDeletedException($"Service with id {command.Id} can't be deleted");
            }

            _applicationDbContext.Services.Remove(service);

            service.AddDomainEvent(new ServiceDeletedEvent(service));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
