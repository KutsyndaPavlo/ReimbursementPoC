using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program.Errors;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeactivateProgram
{
    public class CancelProgramCommandHandler : IRequestHandler<CancelProgramCommand, Result<ProgramDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CancelProgramCommandHandler(IApplicationDbContext applicationDbContext, 
                                           IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<ProgramDto>> Handle(CancelProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FirstOrDefaultAsync(new ProgramByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                return Result<ProgramDto>.Failure(ProgramErrors.NotFound(command.Id));
            }

            entity.Cancel();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProgramDto>(entity);

            return Result<ProgramDto>.Success(dto);
        }
    }
}
