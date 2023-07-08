using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Program.Specification;

namespace ReimbursementPoC.Program.Application.Program.Commands.UpdateProgram
{
    internal class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand, ProgramDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProgramService _ProgramUniquenessChecker;

        public UpdateProgramCommandHandler(
            IApplicationDbContext applicationDbContext,
            IMapper mapper,
            IProgramService ProgramUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _ProgramUniquenessChecker = ProgramUniquenessChecker;
        }

        public async Task<ProgramDto> Handle(UpdateProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FindAsync(new object[] { command.Id }, cancellationToken);

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.Id} doesn't exist.");
            }

            if (command.LastModified.Ticks != entity.LastModified.Ticks)
            {
                throw new ProgramConcurrentUpdateException($"Program {command.Id} version is outdated.");
            }

            entity.UpdateProgram(
                command.Name,
                command.Description,
                command.StateId,
                command.StartDate,
                command.EndDate,
                _ProgramUniquenessChecker);

            _applicationDbContext.Programs.Update(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var result = await _applicationDbContext
                .Programs
                .Include(x => x.State)
                .FirstOrDefaultAsync(new ProgramByIdSpecification(entity.Id).ToExpression());

            var dto = _mapper.Map<ProgramDto>(result);

            return dto;
        }
    }
}
