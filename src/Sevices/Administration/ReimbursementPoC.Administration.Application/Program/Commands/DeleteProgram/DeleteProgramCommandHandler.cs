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
        private readonly IProgramService _ProgramService;

        public DeleteProgramCommandHandler(IApplicationDbContext applicationDbContext, IProgramService ProgramService)
        {
            _applicationDbContext = applicationDbContext;
            _ProgramService = ProgramService;
        }

        public async Task<bool> Handle(DeleteProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FirstOrDefaultAsync(new ProgramByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.Id} doesn't exist");
            }

            if (!entity.CanBeDeleted(_ProgramService))
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
