using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program.Errors;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById
{
    public class GetProgramByIdQueryHandler
        : IRequestHandler<GetProgramByIdQuery, Result<ProgramDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProgramByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<ProgramDto>> Handle(GetProgramByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext
                .Programs
                .Include(x => x.State)
                .Include(x=>x.Services)
                //.ThenInclude(x=>x.Program)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(new ProgramByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                return Result<ProgramDto>.Failure(ProgramErrors.NotFound(query.Id));
            }

            return Result<ProgramDto>.Success(_mapper.Map<ProgramDto>(entity));
        }
    }
}
