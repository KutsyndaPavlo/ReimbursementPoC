using AutoMapper;
using MediatR;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;

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

            if (command.LastModified != entity.LastModified)
            {
                throw new ProgramConcurrentUpdateException($"Program {command.Id} version is outdated.");
            }

            entity.UpdateProgram(
                command.Name,
                command.Description,
                command.State,
                command.StartDate,
                command.EndDate,
                _ProgramUniquenessChecker);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProgramDto>(entity);

            return dto;
        }
    }
}
