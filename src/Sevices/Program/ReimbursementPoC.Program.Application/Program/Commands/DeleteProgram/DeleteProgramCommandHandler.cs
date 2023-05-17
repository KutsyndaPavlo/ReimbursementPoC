using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Administration.Domain.Product.Specification;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Product;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Program.Events;

namespace ReimbursementPoC.Program.Application.Program.Commands.DeleteProgram
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
