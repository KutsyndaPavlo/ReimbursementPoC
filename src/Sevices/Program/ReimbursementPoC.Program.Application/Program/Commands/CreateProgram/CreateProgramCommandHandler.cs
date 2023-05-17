using AutoMapper;
using MediatR;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Program.Commands.CreateProgram
{
    public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, ProgramDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProgramService _ProgramUniquenessChecker;

        public CreateProgramCommandHandler(IApplicationDbContext applicationDbContext, 
                                           IMapper mapper, 
                                           IProgramService ProgramUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _ProgramUniquenessChecker = ProgramUniquenessChecker;
        }

        public async Task<ProgramDto> Handle(CreateProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = ProgramEntity.CreateNew(
                command.Name,
                command.Description,
                command.StartDate,
                command.EndDate,
                command.State,
                _ProgramUniquenessChecker);

            _applicationDbContext.Programs.Add(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProgramDto>(entity);

            return dto;
        }
    }
}
