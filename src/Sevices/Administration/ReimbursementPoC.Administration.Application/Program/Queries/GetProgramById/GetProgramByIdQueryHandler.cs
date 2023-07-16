using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById
{
    public class GetProgramByIdQueryHandler
        : IRequestHandler<GetProgramByIdQuery, ProgramFullDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProgramByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProgramFullDto> Handle(GetProgramByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext
                .Programs
                .Include(x => x.State)
                .Include("_services")
                .FirstOrDefaultAsync(new ProgramByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {query.Id} doesn't exist");
            }

            return _mapper.Map<ProgramFullDto>(entity);
        }
    }
}
