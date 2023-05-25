using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain.Service.Exeption;
using ReimbursementPoC.Program.Domain.Service.Specifications;

namespace ReimbursementPoC.Program.Application.Services.Commands.DeleteService
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

            //if (!entity.CanBeDeleted(_ProgramService))
            //{
            //    throw new ProgramCanNotBeDeletedException($"Program with id {command.Id} can't be deleted");
            //}

            _applicationDbContext.Services.Remove(service);

            //entity.AddDomainEvent(new ProgramDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
