using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain;
using ReimbursementPoC.Administration.Domain.Product;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeleteProgram
{
    public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteProgramCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.Include("_services")
                .FirstOrDefaultAsync(new ProgramByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.Id} doesn't exist");
            }

            if (!entity.CanBeDeleted())
            {
                throw new ProgramCanNotBeDeletedException($"Program with id {command.Id} can't be deleted");
            }

            _applicationDbContext.Programs.Remove(entity);

            entity.AddDomainEvent(new ProgramDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
