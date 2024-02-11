using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program.Errors;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeleteProgram
{
    public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand, Result<bool>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteProgramCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<bool>> Handle(DeleteProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.Include("_services")
                .FirstOrDefaultAsync(new ProgramByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                return Result<bool>.Failure(ProgramErrors.NotFound(command.Id));
            }

            if (!entity.CanBeDeleted())
            {
                return Result<bool>.Failure(ProgramErrors.CanNotBeDeleted(command.Id));
            }

            _applicationDbContext.Programs.Remove(entity);

            entity.AddDomainEvent(new ProgramDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
