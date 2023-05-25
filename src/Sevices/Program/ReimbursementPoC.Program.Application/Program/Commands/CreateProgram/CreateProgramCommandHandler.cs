using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Program.Specification;

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
                command.StateId,
                command.StartDate,
                command.EndDate,
                _ProgramUniquenessChecker);

            _applicationDbContext.Programs.Add(entity);

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
