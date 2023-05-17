using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Administration.Domain.Product.Specification;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain;

namespace ReimbursementPoC.Program.Application.Program.Queries.GetProgramById
{
    public class GetProgramByIdQueryHandler
        : IRequestHandler<GetProgramByIdQuery, ProgramDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetProgramByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProgramDto> Handle(GetProgramByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FirstOrDefaultAsync(new ProgramByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {query.Id} doesn't exist");
            }

            return _mapper.Map<ProgramDto>(entity);
        }
    }
}
